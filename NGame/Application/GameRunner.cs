using NGame.UpdateSchedulers;

namespace NGame.Application;

public interface IGameRunner
{
	void RunGame();
	void Stop();
}



internal sealed class GameRunner : IGameRunner
{
	private readonly IUpdateScheduler _updateScheduler;


	public GameRunner(IUpdateScheduler updateScheduler)
	{
		_updateScheduler = updateScheduler;
	}


	private bool IsClosing { get; set; }


	public void RunGame()
	{
		_updateScheduler.Initialize();
		while (!IsClosing)
		{
			_updateScheduler.Tick();
		}
	}


	public void Stop()
	{
		IsClosing = true;
	}
}
