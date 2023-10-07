namespace NGame.Services;



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
	
	// TODO move probably
	public void SignalGameLoopStopped();
}
