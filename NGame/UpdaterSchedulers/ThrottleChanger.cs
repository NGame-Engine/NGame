namespace NGame.UpdaterSchedulers;

/// <summary>
/// Maybe use later, just stowing it away for now
/// </summary>
public class ThrottleChanger
{
	private readonly IUpdateScheduler _updateScheduler;


	public ThrottleChanger(IUpdateScheduler updateScheduler)
	{
		_updateScheduler = updateScheduler;
		WindowMinimumUpdateRate = new ThreadThrottler(TimeSpan.FromSeconds(0d));
		MinimizedMinimumUpdateRate = new ThreadThrottler(15);
	}


	/// <summary>
	/// Access to the throttler used to set the minimum time allowed between each updates.
	/// </summary>
	public ThreadThrottler WindowMinimumUpdateRate { get; }

	/// <summary>
	/// Access to the throttler used to set the minimum time allowed between each updates while the window is minimized and,
	/// depending on <see cref="TreatNotFocusedLikeMinimized"/>, while unfocused.
	/// </summary>
	public ThreadThrottler MinimizedMinimumUpdateRate { get; }


	public void OnMinimized()
	{
		_updateScheduler.ThreadThrottler = MinimizedMinimumUpdateRate;
	}


	public void OnMaximized()
	{
		_updateScheduler.ThreadThrottler = WindowMinimumUpdateRate;
	}
}
