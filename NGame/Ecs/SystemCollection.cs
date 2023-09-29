using Microsoft.Extensions.Logging;

namespace NGame.Ecs;

public interface ISystemCollection
{
	void Add(ISystem system);
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


	public IEnumerable<ISystem> GetSystems() => _systems.ToList();
}
