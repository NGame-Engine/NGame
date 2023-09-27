using System.Diagnostics;

namespace NGame.UpdateSchedulers
{
	/// <summary>
	/// This provides timing information similar to <see cref="System.Diagnostics.Stopwatch"/> but an update occurring only on a <see cref="Tick"/> method.
	/// </summary>
	public class TimerTick
	{
		private long _startRawTime;
		private long _lastRawTime;
		private int _pauseCount;
		private long _pauseStartTime;
		private long _timePaused;
		private decimal _speedFactor;


		/// <summary>
		/// Initializes a new instance of the <see cref="TimerTick"/> class.
		/// </summary>
		public TimerTick()
		{
			_speedFactor = 1.0m;
			Reset();
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="TimerTick" /> class.
		/// </summary>
		/// <param name="startTime">The start time.</param>
		public TimerTick(TimeSpan startTime)
		{
			_speedFactor = 1.0m;
			Reset(startTime);
		}


		/// <summary>
		/// Gets the start time when this timer was created.
		/// </summary>
		public TimeSpan StartTime { get; private set; }

		/// <summary>
		/// Gets the total time elasped since the last reset or when this timer was created.
		/// </summary>
		public TimeSpan TotalTime { get; private set; }

		/// <summary>
		/// Gets the total time elasped since the last reset or when this timer was created, including <see cref="Pause"/>
		/// </summary>
		public TimeSpan TotalTimeWithPause { get; private set; }

		/// <summary>
		/// Gets the elapsed time since the previous call to <see cref="Tick"/>.
		/// </summary>
		public TimeSpan ElapsedTime { get; private set; }

		/// <summary>
		/// Gets the elapsed time since the previous call to <see cref="Tick"/> including <see cref="Pause"/>
		/// </summary>
		public TimeSpan ElapsedTimeWithPause { get; private set; }

		/// <summary>
		/// Gets or sets the speed factor. Default is 1.0
		/// </summary>
		/// <value>The speed factor.</value>
		public double SpeedFactor
		{
			get
			{
				return (double)_speedFactor;
			}
			set
			{
				_speedFactor = (decimal)value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is paused.
		/// </summary>
		/// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
		public bool IsPaused
		{
			get
			{
				return _pauseCount > 0;
			}
		}


		private static TimeSpan ConvertRawToTimestamp(long delta)
			=> delta == 0 ? default : TimeSpan.FromTicks(delta * TimeSpan.TicksPerSecond / Stopwatch.Frequency);


		/// <summary>
		/// Resets this instance. <see cref="TotalTime"/> is set to zero.
		/// </summary>
		public void Reset()
		{
			Reset(TimeSpan.Zero);
		}


		/// <summary>
		/// Resets this instance. <see cref="TotalTime" /> is set to startTime.
		/// </summary>
		/// <param name="startTime">The start time.</param>
		public void Reset(TimeSpan startTime)
		{
			StartTime = startTime;
			TotalTime = startTime;
			_startRawTime = Stopwatch.GetTimestamp();
			_lastRawTime = _startRawTime;
			_timePaused = 0;
			_pauseStartTime = 0;
			_pauseCount = 0;
		}


		/// <summary>
		/// Resumes this instance, only if a call to <see cref="Pause"/> has been already issued.
		/// </summary>
		public void Resume()
		{
			_pauseCount--;
			if (_pauseCount <= 0)
			{
				_timePaused += Stopwatch.GetTimestamp() - _pauseStartTime;
				_pauseStartTime = 0L;
			}
		}


		/// <summary>
		/// Update the <see cref="TotalTime"/> and <see cref="ElapsedTime"/>,
		/// </summary>
		/// <remarks>
		/// This method must be called on a regular basis at every *tick*.
		/// </remarks>
		public void Tick()
		{
			// Don't tick when this instance is paused.
			if (IsPaused)
			{
				ElapsedTime = TimeSpan.Zero;
				return;
			}

			var rawTime = Stopwatch.GetTimestamp();
			TotalTime = StartTime +
			            new TimeSpan((long)Math.Round(ConvertRawToTimestamp(rawTime - _timePaused - _startRawTime).Ticks *
			                                          _speedFactor));
			TotalTimeWithPause = StartTime +
			                     new TimeSpan((long)Math.Round(ConvertRawToTimestamp(rawTime - _startRawTime).Ticks *
			                                                   _speedFactor));

			ElapsedTime = ConvertRawToTimestamp(rawTime - _timePaused - _lastRawTime);
			ElapsedTimeWithPause = ConvertRawToTimestamp(rawTime - _lastRawTime);

			if (ElapsedTime < TimeSpan.Zero)
			{
				ElapsedTime = TimeSpan.Zero;
			}

			_lastRawTime = rawTime;
		}


		/// <summary>
		/// Pauses this instance.
		/// </summary>
		public void Pause()
		{
			_pauseCount++;
			if (_pauseCount == 1)
			{
				_pauseStartTime = Stopwatch.GetTimestamp();
			}
		}
	}
}
