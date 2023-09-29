using Microsoft.Extensions.Logging;
using NGame.UpdateSchedulers;

namespace NGame.Ecs;

public interface ISystemCollection
{
	void Add(ISystem system);
	void UpdateSystems(GameTime gameTime);
	IEnumerable<ISystem> GetSystems();
}



public class SystemCollection : ISystemCollection
{
	private readonly ILogger<SystemCollection> _logger;
	private readonly ICollection<ISystem> _systems = new List<ISystem>();


	public SystemCollection(ILogger<SystemCollection> logger)
	{
		_logger = logger;
	}


	public void Add(ISystem system)
	{
		_systems.Add(system);
		_logger.LogInformation("System {0} added", system);
	}


	public void UpdateSystems(GameTime gameTime)
	{
		foreach (var system in _systems)
		{
			system.Update(gameTime);
		}
	}


	public IEnumerable<ISystem> GetSystems() => _systems.ToList();
}
