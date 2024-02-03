using Microsoft.Extensions.Logging;
using NGame.UpdateLoop;

namespace NGame.Implementations.UpdateLoop;



public class TickScheduler : ITickScheduler
{
	private readonly ILogger<TickScheduler> _logger;
	private readonly IRenderContext _renderContext;
	private readonly IEnumerable<IUpdatable> _updatables;
	private readonly IEnumerable<IDrawable> _drawables;


	private readonly TimeSpan _maximumElapsedTime = TimeSpan.FromMilliseconds(500.0);
	private readonly TimerTick _autoTickTimer = new();
	private readonly object _tickLock = new();


	public TickScheduler(
		ILogger<TickScheduler> logger,
		IRenderContext renderContext,
		IEnumerable<IDrawable> drawables,
		IEnumerable<IUpdatable> updatables
	)
	{
		_logger = logger;
		_renderContext = renderContext;
		_updatables = updatables.OrderBy(x => x.Order);
		_drawables = drawables.OrderBy(x => x.Order);
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


	void ITickScheduler.Initialize()
	{
		try
		{
			_renderContext.BeginDraw();

			_autoTickTimer.Reset();
			UpdateLoopTime.Reset(UpdateLoopTime.Total);

			_renderContext.EndDraw(false);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unexpected exception");
			throw;
		}
	}


	void ITickScheduler.Tick()
	{
		lock (_tickLock)
		{
			if (IsStopped) return;

			try
			{
				var tickTiming = CalculateTickTiming();
				if (tickTiming.UpdateCount == 0) return;

				ExecuteTick(tickTiming);
				ThreadThrottler.Throttle(out long _);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected exception");
				throw;
			}
		}

		HasFinishedFirstUpdateLoop = true;
	}


	private TickTiming CalculateTickTiming()
	{
		_autoTickTimer.Tick();

		var elapsedAdjustedTime = _autoTickTimer.ElapsedTimeWithPause;

		if (elapsedAdjustedTime > _maximumElapsedTime)
		{
			elapsedAdjustedTime = _maximumElapsedTime;
		}

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
				return new TickTiming(TimeSpan.Zero, 0, 0f);
			}

			// We are going to call Update updateCount times, so we can subtract this from accumulated elapsed game time
			AccumulatedElapsedGameTime =
				new TimeSpan(AccumulatedElapsedGameTime.Ticks - (updateCount * FixedTimeStepTarget.Ticks));
			singleFrameElapsedTime = FixedTimeStepTarget;
		}

		var drawInterpolationFactor = drawLag / (float)FixedTimeStepTarget.Ticks;
		return new TickTiming(singleFrameElapsedTime, updateCount, drawInterpolationFactor);
	}


	// TODO split off into own class
	private void ExecuteTick(TickTiming tickTiming)
	{
		bool beginDrawSuccessful = false;
		try
		{
			var updateCount = tickTiming.UpdateCount;
			var elapsedTimePerUpdate = tickTiming.ElapsedTimePerUpdate;
			var drawInterpolationFactor = tickTiming.DrawInterpolationFactor;

			var totalElapsedTime = TimeSpan.Zero;
			beginDrawSuccessful = _renderContext.BeginDraw();

			// Reset the time of the next frame
			for (int i = 0; i < updateCount && !IsStopped; i++)
			{
				UpdateLoopTime.Update(UpdateLoopTime.Total + elapsedTimePerUpdate, elapsedTimePerUpdate, true);

				foreach (var updatable in _updatables)
				{
					updatable.Update(UpdateLoopTime);
				}

				totalElapsedTime += elapsedTimePerUpdate;
			}

			if (IsStopped || !HasFinishedFirstUpdateLoop) return;

			DrawInterpolationFactor = drawInterpolationFactor;
			DrawLoopTime.Factor = UpdateLoopTime.Factor;
			DrawLoopTime.Update(DrawLoopTime.Total + totalElapsedTime, totalElapsedTime, true);

			foreach (var drawable in _drawables)
			{
				drawable.Draw(DrawLoopTime);
			}
		}
		finally
		{
			if (beginDrawSuccessful)
			{
				_renderContext.EndDraw(true);
			}
		}
	}



	private struct TickTiming(TimeSpan elapsedTimePerUpdate, int updateCount, float drawInterpolationFactor)
	{
		public readonly TimeSpan ElapsedTimePerUpdate = elapsedTimePerUpdate;
		public readonly int UpdateCount = updateCount;
		public readonly float DrawInterpolationFactor = drawInterpolationFactor;
	}
}
