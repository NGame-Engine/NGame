using NGame.GameLoop;

namespace NGame.Application;



public interface IGameRunner
{
	void RunGame();
	void Stop();
}



internal sealed class GameRunner : IGameRunner
{
	private readonly IUpdateScheduler _updateScheduler;
	private readonly IApplicationEvents _applicationEvents;


	public GameRunner(IUpdateScheduler updateScheduler, IApplicationEvents applicationEvents)
	{
		_updateScheduler = updateScheduler;
		_applicationEvents = applicationEvents;
	}


	private bool IsClosing { get; set; }


	public void RunGame()
	{
		_updateScheduler.Initialize();
		while (!IsClosing)
		{
			_updateScheduler.Tick();
		}

		_applicationEvents.SignalGameLoopStopped();
	}


	public void Stop()
	{
		IsClosing = true;
	}
}
