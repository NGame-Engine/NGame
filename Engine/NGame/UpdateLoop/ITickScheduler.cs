namespace NGame.UpdateLoop;



public interface ITickScheduler
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

	void Initialize();
	void Tick();
}
