using Microsoft.Extensions.Logging;
using NGame.OsWindows;
using NGame.Renderers;

namespace NGame.UpdateSchedulers;

public interface IUpdateScheduler
{
	bool IsStopped { get; set; }
	GameTime UpdateLoopTime { get; }
	GameTime DrawLoopTime { get; }
	ThreadThrottler ThreadThrottler { get; set; }
	bool HasFinishedFirstUpdateLoop { get; }

	/// <summary>
	/// Gets or sets a value indicating whether the elapsed time between each update should be constant,
	/// see <see cref="FixedTimeStepTarget"/> to configure the duration.
	/// </summary>
	bool IsFixedTimeStep { get; set; }

	/// <summary>
	/// Gets or sets the target elapsed time, this is the duration of each tick/update
	/// when <see cref="IsFixedTimeStep"/> is enabled.
	/// </summary>
	TimeSpan FixedTimeStepTarget { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance should force exactly one update step per one draw step
	/// </summary>
	bool ForceOneUpdatePerDraw { get; set; }

	bool AllowDrawingBetweenFixedTimeSteps { get; set; }

	/// <summary>
	/// Is used when we draw without running an update beforehand, so when both <see cref="IsFixedTimeStep"/> 
	/// and <see cref="AllowDrawingBetweenFixedTimeSteps"/> are set.<br/>
	/// It returns a number between 0 and 1 which represents the current position our DrawTime is in relation 
	/// to the previous and next step.<br/>
	/// 0.5 would mean that we are rendering at a time halfway between the previous and next fixed-step.
	/// </summary>
	float DrawInterpolationFactor { get; }

	void Initialize(CancellationTokenSource cancellationTokenSource);
	void Tick();
}



public class UpdateScheduler : IUpdateScheduler
{
	private readonly ILogger<UpdateScheduler> _logger;
	private readonly INGameRenderer _nGameRenderer;
	private readonly IUpdatableCollection _updatableCollection;
	private readonly IOsWindow _osWindow;

	private readonly TimeSpan _maximumElapsedTime = TimeSpan.FromMilliseconds(500.0);
	private readonly TimerTick _autoTickTimer = new();
	private readonly object _tickLock = new();


	public UpdateScheduler(
		ILogger<UpdateScheduler> logger,
		INGameRenderer nGameRenderer,
		IUpdatableCollection updatableCollection,
		IOsWindow osWindow
	)
	{
		_logger = logger;
		_nGameRenderer = nGameRenderer;
		_updatableCollection = updatableCollection;
		_osWindow = osWindow;
	}


	public bool IsStopped { get; set; }
	public GameTime UpdateLoopTime { get; } = new();
	public GameTime DrawLoopTime { get; } = new();
	public ThreadThrottler ThreadThrottler { get; set; } = new(TimeSpan.FromSeconds(0d));
	public bool HasFinishedFirstUpdateLoop { get; private set; }


	public bool IsFixedTimeStep { get; set; } = false;
	public TimeSpan FixedTimeStepTarget { get; set; } = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
	public bool ForceOneUpdatePerDraw { get; set; }
	public bool AllowDrawingBetweenFixedTimeSteps { get; set; }


	public float DrawInterpolationFactor { get; private set; }

	private TimeSpan AccumulatedElapsedGameTime { get; set; }


	void IUpdateScheduler.Initialize(CancellationTokenSource cancellationTokenSource)
	{
		try
		{
			_updatableCollection.Initialize();

			_osWindow.Initialize(cancellationTokenSource);
			_nGameRenderer.Initialize();

			_nGameRenderer.BeginDraw();

			_autoTickTimer.Reset();
			UpdateLoopTime.Reset(UpdateLoopTime.Total);

			_nGameRenderer.EndDraw(false);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unexpected exception");
			throw;
		}
	}


	void IUpdateScheduler.Tick()
	{
		lock (_tickLock)
		{
			if (IsStopped)
			{
				return;
			}

			RawTickProducer();
		}

		HasFinishedFirstUpdateLoop = true;
	}


	private async void RawTickProducer()
	{
		try
		{
			// Update the timer
			_autoTickTimer.Tick();

			var elapsedAdjustedTime = _autoTickTimer.ElapsedTimeWithPause;

			if (elapsedAdjustedTime > _maximumElapsedTime)
			{
				elapsedAdjustedTime = _maximumElapsedTime;
			}

			bool drawFrame = true;
			int updateCount = 1;
			var singleFrameElapsedTime = elapsedAdjustedTime;
			var drawLag = 0L;


			if (IsFixedTimeStep)
			{
				// If the rounded TargetElapsedTime is equivalent to current ElapsedAdjustedTime
				// then make ElapsedAdjustedTime = TargetElapsedTime. We take the same internal rules as XNA
				if (Math.Abs(elapsedAdjustedTime.Ticks - FixedTimeStepTarget.Ticks) < (FixedTimeStepTarget.Ticks >> 6))
				{
					elapsedAdjustedTime = FixedTimeStepTarget;
				}

				// Update the accumulated time
				AccumulatedElapsedGameTime += elapsedAdjustedTime;

				// Calculate the number of update to issue
				updateCount =
					ForceOneUpdatePerDraw
						? 1
						: (int)(AccumulatedElapsedGameTime.Ticks / FixedTimeStepTarget.Ticks);

				if (AllowDrawingBetweenFixedTimeSteps)
				{
					drawLag = AccumulatedElapsedGameTime.Ticks % FixedTimeStepTarget.Ticks;
				}
				else if (updateCount == 0)
				{
					return;
				}

				// We are going to call Update updateCount times, so we can subtract this from accumulated elapsed game time
				AccumulatedElapsedGameTime =
					new TimeSpan(AccumulatedElapsedGameTime.Ticks - (updateCount * FixedTimeStepTarget.Ticks));
				singleFrameElapsedTime = FixedTimeStepTarget;
			}

			var drawInterpolationFactor = drawLag / (float)FixedTimeStepTarget.Ticks;
			await RawTick(singleFrameElapsedTime, updateCount, drawInterpolationFactor, drawFrame);


			ThreadThrottler.Throttle(out long _);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unexpected exception");
			throw;
		}
	}


	private async Task RawTick(
		TimeSpan elapsedTimePerUpdate,
		int updateCount = 1,
		float drawInterpolationFactor = 0,
		bool drawFrame = true
	)
	{
		bool beginDrawSuccessful = false;
		TimeSpan totalElapsedTime = TimeSpan.Zero;
		try
		{
			beginDrawSuccessful = _nGameRenderer.BeginDraw();

			// Reset the time of the next frame
			for (int i = 0; i < updateCount && !IsStopped; i++)
			{
				UpdateLoopTime.Update(UpdateLoopTime.Total + elapsedTimePerUpdate, elapsedTimePerUpdate, true);
				await _updatableCollection.Update(UpdateLoopTime);
				totalElapsedTime += elapsedTimePerUpdate;
			}

			if (drawFrame && !IsStopped && HasFinishedFirstUpdateLoop)
			{
				DrawInterpolationFactor = drawInterpolationFactor;
				DrawLoopTime.Factor = UpdateLoopTime.Factor;
				DrawLoopTime.Update(DrawLoopTime.Total + totalElapsedTime, totalElapsedTime, true);

				_nGameRenderer.Draw(DrawLoopTime);
			}
		}
		finally
		{
			if (beginDrawSuccessful)
			{
				_nGameRenderer.EndDraw(true);
			}
		}
	}
}
