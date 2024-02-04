using NGame.Setup;

namespace NGame.Platform.Setup;



public class DefaultNGameApplication : INGameApplication
{
	public event Action? Started;
	public event Action? Deactivated;
	public event Action? Stopped;
	public event Action? Resumed;
	public event Action<DestroyRequestedEventArgs>? DestroyRequested;
	public event Action? Destroying;

	public WindowStatus WindowStatus { get; protected set; } = WindowStatus.Starting;


	public virtual void Start()
	{
		Started?.Invoke();
		WindowStatus = WindowStatus.Running;
	}


	public virtual void Deactivate()
	{
		Deactivated?.Invoke();
		WindowStatus = WindowStatus.Deactivated;
	}


	public virtual void Stop()
	{
		if (WindowStatus == WindowStatus.Running) Deactivate();

		Stopped?.Invoke();
		WindowStatus = WindowStatus.Stopped;
	}


	public virtual void Resume()
	{
		Resumed?.Invoke();
		WindowStatus = WindowStatus.Running;
	}


	public virtual void RequestDestroy()
	{
		var eventArgs = new DestroyRequestedEventArgs();
		DestroyRequested?.Invoke(eventArgs);

		if (!eventArgs.IsGoingToClose) return;

		Destroy();
	}


	public virtual void Destroy()
	{
		if (WindowStatus != WindowStatus.Stopped) Stop();
		Destroying?.Invoke();
	}
}
