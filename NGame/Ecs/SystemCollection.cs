using Microsoft.Extensions.Logging;

namespace NGame.Ecs;



public interface ISystemCollection
{
	void Add(ISystem system);
	IEnumerable<ISystem> GetSystems();

	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);

	void AddComponent(Entity sender);
	void RemoveComponent(Entity sender);
}



internal class SystemCollection : ISystemCollection
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
		_logger.LogInformation("System {System} added", system);
	}


	public IEnumerable<ISystem> GetSystems() => _systems.ToList();


	public void AddEntity(Entity entity)
	{
		var componentTypes = GetComponentTypes(entity);

		foreach (var system in _systems)
		{
			if (!system.EntityIsMatch(componentTypes)) continue;

			system.Add(entity);
		}
	}


	public void RemoveEntity(Entity entity)
	{
		foreach (var system in _systems)
		{
			system.Remove(entity);
		}
	}


	public void AddComponent(Entity entity)
	{
		var componentTypes = GetComponentTypes(entity);

		foreach (var system in _systems)
		{
			if (system.Contains(entity)) continue;
			if (!system.EntityIsMatch(componentTypes)) continue;

			system.Add(entity);
		}
	}


	public void RemoveComponent(Entity entity)
	{
		var componentTypes = GetComponentTypes(entity);

		foreach (var system in _systems)
		{
			if (!system.Contains(entity)) continue;
			if (system.EntityIsMatch(componentTypes)) continue;

			system.Remove(entity);
		}
	}


	private static HashSet<Type> GetComponentTypes(Entity entity) =>
		entity
			.GetComponents()
			.Select(x => x.GetType())
			.ToHashSet();
}
