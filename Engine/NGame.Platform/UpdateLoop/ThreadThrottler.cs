using System.Diagnostics;

namespace NGame.Platform.UpdateLoop;



public sealed class ThreadThrottler
{
	/// <summary>
	/// Set this to zero to disable throttling.
	/// Minimum amount of time allowed between each 'update'.
	/// Conversion is lossy, getting this value back might not return the same value you set it to.
	/// </summary>
	/// <remarks>
	/// See <see cref="Throttle(out long)"/>'s summary for an idea of how this property is used.
	/// </remarks>
	public TimeSpan MinimumElapsedTime
	{
		get => ToSpan(_periodDuration);
		set => _periodDuration = (long)((double)Stopwatch.Frequency / TimeSpan.TicksPerSecond * value.Ticks);
	}

	/// <summary>
	/// The type of throttler used
	/// </summary>
	public ThrottlerType Type { get; private set; }

	private long _stamp;
	private long _periodDuration;
	private long _error;
	private double _spinwaitWindow;


	/// <summary>
	/// Create an instance of this class without throttling and defaulting to <see cref="ThrottlerType.Standard"/> mode.
	/// See <see cref="SetToPreciseAuto"/> and <see cref="SetToPreciseManual"/> to set it to other modes.
	/// </summary>
	public ThreadThrottler()
	{
		_stamp = Stopwatch.GetTimestamp();
		SetToStandard();
	}


	/// <summary>
	/// Create an instance of this class set to <see cref="ThrottlerType.Standard"/> mode.
	/// See <see cref="SetToPreciseAuto"/> and <see cref="SetToPreciseManual"/> to set it to other modes.
	/// </summary>
	/// <param name="minimumElapsedTimeParam">Minimum time allowed between each call</param>
	public ThreadThrottler(TimeSpan minimumElapsedTimeParam) : this()
	{
		MinimumElapsedTime = minimumElapsedTimeParam;
	}


	/// <summary>
	/// Create an instance of this class set to <see cref="ThrottlerType.Standard"/> mode.
	/// See <see cref="SetToPreciseAuto"/> and <see cref="SetToPreciseManual"/> to set it to other modes.
	/// </summary>
	/// <param name="frequencyMax">The maximum frequency this object allows</param>
	public ThreadThrottler(int frequencyMax) : this()
	{
		SetMaxFrequency(frequencyMax);
	}


	/// <summary>
	/// Thread will be blocked to stay within the given amount of 'updates' per second.
	/// </summary>
	/// <remarks>
	/// It effectively transforms the given parameter from frame per second
	/// to the internal closest second per frame equivalent for it.
	/// </remarks>
	public void SetMaxFrequency(int frequencyMax)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(frequencyMax);
		_periodDuration = (long)(Stopwatch.Frequency / (double)frequencyMax);
	}


	/// <summary>
	/// Saves CPU cycles while waiting, this one is the least precise mode but the lightest.
	/// <para/>
	/// If you aren't sure, use this one !
	/// </summary>
	public void SetToStandard()
	{
		Type = ThrottlerType.Standard;
		_spinwaitWindow = 0;
	}


	/// <summary>
	/// Most of the timings will be perfectly precise to the system timer at the cost of higher CPU usage.
	/// <para/>
	/// If you aren't sure, use <see cref="SetToStandard"/> instead.
	/// </summary>
	/// <remarks>
	/// This mode uses and automatically scales a spinwait window based on system responsiveness to find the right balance
	/// between Thread.Sleep calls and spinwaiting.
	/// </remarks>
	public void SetToPreciseAuto()
	{
		// Avoid window reset if already precise auto
		if (Type == ThrottlerType.PreciseAuto)
			return;
		Type = ThrottlerType.PreciseAuto;
		_spinwaitWindow = 0;
	}


	/// <summary>
	/// Depending on the provided value, timings will be perfectly precise to the system timer at the cost of higher CPU usage.
	/// <para/>
	/// If you aren't sure, use <see cref="SetToStandard"/> instead.
	/// </summary>
	/// <remarks>
	/// This mode uses the given value as the duration of the spinwait window.
	/// The format of this value is based on <see cref="Stopwatch"/>.<see cref="Stopwatch.Frequency"/>
	/// </remarks>
	public void SetToPreciseManual(long spinwaitWindowParam)
	{
		Type = ThrottlerType.PreciseManual;
		_spinwaitWindow = spinwaitWindowParam;
	}


	/// <summary>
	/// Forces the thread to sleep when the time elapsed since last call is lower than <see cref="MinimumElapsedTime"/>,
	/// it will sleep for the time remaining to reach <see cref="MinimumElapsedTime"/>.
	/// <para/> 
	/// Use this function inside a loop when you want to lock it to a specific rate.
	/// </summary>
	/// <param name="elapsedTimeSpan">
	/// The time since the last call,
	/// returns a value close to <see cref="MinimumElapsedTime"/>, 
	/// use this value as your delta time.
	/// </param>
	/// <returns><c>True</c> if this class had to throttle, <c>false</c> otherwise</returns>
	public bool Throttle(out TimeSpan elapsedTimeSpan)
	{
		var r = Throttle(out long stamp);
		elapsedTimeSpan = ToSpan(stamp);
		return r;
	}


	/// <summary>
	/// Forces the thread to sleep when the time elapsed since last call is lower than <see cref="MinimumElapsedTime"/>,
	/// it will sleep for the time remaining to reach <see cref="MinimumElapsedTime"/>.
	/// <para/> 
	/// Use this function inside a loop when you want to lock it to a specific rate.
	/// </summary>
	/// <param name="elapsedInSeconds">
	/// The time since the last call in seconds,
	/// returns a value close to <see cref="MinimumElapsedTime"/>, 
	/// use this value as your delta time.
	/// </param>
	/// <returns><c>True</c> if this class had to throttle, <c>false</c> otherwise</returns>
	public bool Throttle(out double elapsedInSeconds)
	{
		var r = Throttle(out long outStamp);
		elapsedInSeconds = (double)outStamp / Stopwatch.Frequency;
		return r;
	}


	/// <summary>
	/// Forces the thread to sleep when the time elapsed since last call is lower than
	/// <see cref="MinimumElapsedTime"/>, it will sleep for the time remaining to reach that value.
	/// <para/> 
	/// Use this function inside a loop when you want to lock it to a specific rate.
	/// </summary>
	/// <param name="elapsedInSwFreq">
	/// The time since the last call in <see cref="Stopwatch"/>.<see cref="Stopwatch.Frequency"/>
	/// returns a value close to <see cref="MinimumElapsedTime"/>, 
	/// use this value as your delta time.
	/// </param>
	/// <returns><c>True</c> if this class had to throttle, <c>false</c> otherwise</returns>
	public bool Throttle(out long elapsedInSwFreq)
	{
		bool throttled = false;
		try
		{
			// Reduce window to account for extreme cases
			if (Type == ThrottlerType.PreciseAuto)
			{
				_spinwaitWindow -= _spinwaitWindow / 10;
			}

			var oneMs = Stopwatch.Frequency / 1000d;
			while (true)
			{
				var idleDuration = _periodDuration - (Stopwatch.GetTimestamp() - _stamp - _error + _spinwaitWindow);
				if (idleDuration < oneMs) // Less than one ms, exit sleep loop
				{
					if (Type == ThrottlerType.Standard)
						return false;
					break;
				}

				throttled = true;
				if (Type == ThrottlerType.PreciseAuto)
				{
					var sleepStart = Stopwatch.GetTimestamp();
					Thread.Sleep(1);
					// Include excessive time sleep took on top of the time we specified
					_spinwaitWindow += Stopwatch.GetTimestamp() - sleepStart - oneMs;
					// Average to account for general system responsiveness
					_spinwaitWindow /= 2d;
				}
				else if (Type == ThrottlerType.PreciseManual)
				{
					Thread.Sleep(1);
				}
				else
				{
					Thread.Sleep((int)(idleDuration / oneMs));
					return true; // Don't let standard spinwait
				}
			}

			var goalStamp = _stamp + _periodDuration + _error;
			if (Stopwatch.GetTimestamp() >= goalStamp) return throttled;


			while (Stopwatch.GetTimestamp() < goalStamp) { }

			return true;
		}
		finally
		{
			var newStamp = Stopwatch.GetTimestamp();
			elapsedInSwFreq = newStamp - _stamp;
			_stamp = newStamp;
			if (throttled)
			{
				// Include time to catch-up or loose when our waiting method is lossy
				_error += _periodDuration - elapsedInSwFreq;
			}
			else
			{
				_error = 0;
			}
		}
	}


	static TimeSpan ToSpan(long stamp)
	{
		return new TimeSpan(stamp == 0 ? 0 : (long)(stamp * TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency));
	}



	public enum ThrottlerType
	{
		Standard,
		PreciseManual,
		PreciseAuto
	}
}
