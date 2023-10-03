namespace NGame.UpdateSchedulers;



public interface IFrameEventBus
{
	void DoAtNextFrameStart(Action action);
	void OnFrameStart();
}



public class FrameEventBus : IFrameEventBus
{
	private readonly List<Action> _actionsForNextFrameStart = new();


	public void DoAtNextFrameStart(Action action)
	{
		_actionsForNextFrameStart.Add(action);
	}


	public void OnFrameStart()
	{
		foreach (var action in _actionsForNextFrameStart)
		{
			action();
		}

		_actionsForNextFrameStart.Clear();
	}
}
