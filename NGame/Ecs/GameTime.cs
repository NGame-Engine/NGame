namespace NGame.Ecs;

/// <summary>
/// Current timing used for variable-step (real time) or fixed-step (game time) games.
/// </summary>
public sealed class GameTime
{
	private TimeSpan _accumulatedElapsedTime;
	private int _accumulatedFrameCountPerSecond;
	private double _factor;


	/// <summary>
	/// Initializes a new instance of the <see cref="GameTime" /> class.
	/// </summary>
	public GameTime()
	{
		_accumulatedElapsedTime = TimeSpan.Zero;
		_factor = 1;
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="GameTime" /> class.
	/// </summary>
	/// <param name="totalTime">The total game time since the start of the game.</param>
	/// <param name="elapsedTime">The elapsed game time since the last update.</param>
	public GameTime(TimeSpan totalTime, TimeSpan elapsedTime)
	{
		Total = totalTime;
		Elapsed = elapsedTime;
		_accumulatedElapsedTime = TimeSpan.Zero;
		_factor = 1;
	}


	/// <summary>
	/// Gets the elapsed game time since the last update
	/// </summary>
	/// <value>The elapsed game time.</value>
	public TimeSpan Elapsed { get; private set; }

	/// <summary>
	/// Gets the amount of game time since the start of the game.
	/// </summary>
	/// <value>The total game time.</value>
	public TimeSpan Total { get; private set; }

	/// <summary>
	/// Gets the current frame count since the start of the game.
	/// </summary>
	public int FrameCount { get; private set; }

	/// <summary>
	/// Gets the number of frame per second (FPS) for the current running game.
	/// </summary>
	/// <value>The frame per second.</value>
	public float FramePerSecond { get; private set; }

	/// <summary>
	/// Gets the time per frame.
	/// </summary>
	/// <value>The time per frame.</value>
	public TimeSpan TimePerFrame { get; private set; }

	/// <summary>
	/// Gets a value indicating whether the <see cref="FramePerSecond"/> and <see cref="TimePerFrame"/> were updated for this frame.
	/// </summary>
	/// <value><c>true</c> if the <see cref="FramePerSecond"/> and <see cref="TimePerFrame"/> were updated for this frame; otherwise, <c>false</c>.</value>
	public bool FramePerSecondUpdated { get; private set; }


	/// <summary>
	/// Gets the amount of time elapsed multiplied by the time factor.
	/// </summary>
	/// <value>The warped elapsed time</value>
	public TimeSpan WarpElapsed { get; private set; }


	/// <summary>
	/// Gets or sets the time factor.<br/>
	/// This value controls how much the warped time flows, this includes physics, animations and particles.
	/// A value between 0 and 1 will slow time, a value above 1 will make it faster.
	/// </summary>
	/// <value>The multiply factor, a double value higher or equal to 0</value>
	public double Factor
	{
		get => _factor;
		set => _factor = value > 0 ? value : 0;
	}


	public void Update(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool incrementFrameCount)
	{
		Total = totalGameTime;
		Elapsed = elapsedGameTime;
		WarpElapsed = TimeSpan.FromTicks((long)(Elapsed.Ticks * Factor));

		FramePerSecondUpdated = false;

		if (!incrementFrameCount) return;


		_accumulatedElapsedTime += elapsedGameTime;
		var accumulatedElapsedGameTimeInSecond = _accumulatedElapsedTime.TotalSeconds;
		if (_accumulatedFrameCountPerSecond > 0 && accumulatedElapsedGameTimeInSecond > 1.0)
		{
			TimePerFrame = TimeSpan.FromTicks(_accumulatedElapsedTime.Ticks / _accumulatedFrameCountPerSecond);
			FramePerSecond = (float)(_accumulatedFrameCountPerSecond / accumulatedElapsedGameTimeInSecond);
			_accumulatedFrameCountPerSecond = 0;
			_accumulatedElapsedTime = TimeSpan.Zero;
			FramePerSecondUpdated = true;
		}

		_accumulatedFrameCountPerSecond++;
		FrameCount++;
	}


	public void Reset(TimeSpan totalGameTime)
	{
		Update(totalGameTime, TimeSpan.Zero, false);
		_accumulatedElapsedTime = TimeSpan.Zero;
		_accumulatedFrameCountPerSecond = 0;
		FrameCount = 0;
	}


	public void ResetTimeFactor()
	{
		_factor = 1;
	}
}
