namespace NGame.Application;



public sealed class CloseRequestedEventArgs : EventArgs
{
	public bool IsGoingToClose { get; set; } = true;
}



public interface IApplicationEvents
{
	event EventHandler<CloseRequestedEventArgs> CloseRequested;
	event EventHandler Closing;
	public event EventHandler GameLoopStopped;

	void RequestClose();
	internal void SignalGameLoopStopped();
}



internal class ApplicationEvents : IApplicationEvents
{
	public event EventHandler<CloseRequestedEventArgs>? CloseRequested;
	public event EventHandler? Closing;
	public event EventHandler? GameLoopStopped;


	public void RequestClose()
	{
		var eventArgs = new CloseRequestedEventArgs();
		CloseRequested?.Invoke(this, eventArgs);

		if (!eventArgs.IsGoingToClose) return;

		Closing?.Invoke(this, EventArgs.Empty);
	}


	public void SignalGameLoopStopped()
	{
		GameLoopStopped?.Invoke(this, EventArgs.Empty);
	}
}
