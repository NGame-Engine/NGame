namespace NGame.Services;



public enum WindowStatus
{
	Starting,
	Running,
	Deactivated,
	Stopped
}



public sealed class DestroyRequestedEventArgs : EventArgs
{
	/// <summary>
	/// Can be set to false to prevent the app from closing
	/// (if the platform supports that).
	/// </summary>
	public bool IsGoingToClose { get; set; } = true;
}



public interface INGameApplication
{
	/// <summary>
	/// The application is starting.
	/// 
	/// <see cref="WindowStatus"/> will be set to
	/// <see cref="WindowStatus.Running"/> when finished.
	/// </summary>
	event Action Started;


	/// <summary>
	/// The application has lost focus, but is still visible to the user.
	/// 
	/// <see cref="WindowStatus"/> will be set to
	/// <see cref="WindowStatus.Deactivated"/> when finished.
	/// </summary>
	event Action Deactivated;

	/// <summary>
	/// The application is no longer visible to the user,
	/// but still running in the background.
	/// 
	/// If <see cref="WindowStatus"/> was <see cref="WindowStatus.Running"/> before,
	/// the <see cref="Deactivated"/> event is raised before this.
	/// 
	/// <see cref="WindowStatus"/> will be set to
	/// <see cref="WindowStatus.Stopped"/> when finished.
	/// </summary>
	event Action Stopped;

	/// <summary>
	/// The application went back to being the active application with focus.
	/// 
	/// <see cref="WindowStatus"/> will be set to
	/// <see cref="WindowStatus.Running"/> when finished.
	/// </summary>
	event Action Resumed;

	/// <summary>
	/// A user clicked the button to close the application.
	/// By setting <see cref="DestroyRequestedEventArgs.IsGoingToClose"/>
	/// to false in this event's argument, the application can be prevented
	/// from closing if the platform supports that.
	/// </summary>
	event Action<DestroyRequestedEventArgs> DestroyRequested;

	/// <summary>
	/// The application is preparing from being removed from memory.
	/// At this point, the game loop does not run anymore.
	/// 
	/// If <see cref="WindowStatus"/> was <see cref="WindowStatus.Deactivated"/> before,
	/// the <see cref="Stopped"/> event is raised before this.
	/// 
	/// If <see cref="WindowStatus"/> was <see cref="WindowStatus.Running"/> before,
	/// the <see cref="Deactivated"/> and the <see cref="Stopped"/> events
	/// are raised before this.
	/// </summary>
	event Action Destroying;


	public WindowStatus WindowStatus { get; }
}
