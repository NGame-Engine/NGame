using Microsoft.Extensions.Logging;
using NGame.Ecs;

namespace NGame.UpdateSchedulers;

public interface IUpdatableCollection
{
	void Initialize();
	void Update(GameTime gameTime);
}



public class UpdatableCollection : IUpdatableCollection
{
	private readonly ILogger<UpdatableCollection> _logger;
	private readonly ISystemCollection _systemCollection;


	public UpdatableCollection(ILogger<UpdatableCollection> logger, ISystemCollection systemCollection)
	{
		_logger = logger;
		_systemCollection = systemCollection;
	}


	public void Initialize()
	{
		_logger.LogInformation("Initialize");
	}


	public void Update(GameTime gameTime)
	{
		_systemCollection.UpdateSystems(gameTime);
	}
}
