namespace NGame.Application;

public class CloseRequestedEventArgs : EventArgs
{
	public bool IsGoingToClose { get; set; } = true;
}



public interface IApplicationEvents
{
	event EventHandler<CloseRequestedEventArgs> CloseRequested;
	event EventHandler Closing;

	void RequestClose();
}



internal class ApplicationEvents : IApplicationEvents
{
	public event EventHandler<CloseRequestedEventArgs>? CloseRequested;
	public event EventHandler? Closing;


	public void RequestClose()
	{
		var eventArgs = new CloseRequestedEventArgs();
		CloseRequested?.Invoke(this, eventArgs);

		if (!eventArgs.IsGoingToClose) return;

		Closing?.Invoke(this, EventArgs.Empty);
	}
}
