using Microsoft.Extensions.Logging;
using NGame.UpdaterSchedulers;

namespace NGame.Ecs;

public interface ISystemCollection
{
	void Add(ISystem system);
	Task UpdateSystems(GameTime gameTime, CancellationToken cancellationToken);
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


	public async Task UpdateSystems(GameTime gameTime, CancellationToken cancellationToken)
	{
		foreach (var system in _systems)
		{
			await system.Update(gameTime, cancellationToken);
		}
	}


	public IEnumerable<ISystem> GetSystems() => _systems.ToList();
}
