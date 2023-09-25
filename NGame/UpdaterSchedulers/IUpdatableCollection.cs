using Microsoft.Extensions.Logging;

namespace NGame.UpdaterSchedulers;

public interface IUpdatableCollection
{
	void Initialize();
	Task Update(GameTime gameTime);
}



public class UpdatableCollection : IUpdatableCollection
{
	private readonly ILogger<UpdatableCollection> _logger;


	public UpdatableCollection(ILogger<UpdatableCollection> logger)
	{
		_logger = logger;
	}


	public void Initialize()
	{
		_logger.LogInformation("Initialize");
	}


	public Task Update(GameTime gameTime)
	{
		_logger.LogInformation("Update at {0}", gameTime.Total);
		return Task.CompletedTask;
	}
}
