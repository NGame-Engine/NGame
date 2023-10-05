using Microsoft.Extensions.Logging;

namespace NGame.Ecs;



public interface ISystemCollection
{
	void AddSystem(ISystem system);

	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);

	void AddComponent(Entity sender);
	void RemoveComponent(Entity sender);
}



internal class SystemCollection : ISystemCollection
{
	private readonly ILogger<SystemCollection> _logger;
	private readonly List<ISystem> _systems = new();


	public SystemCollection(ILogger<SystemCollection> logger)
	{
		_logger = logger;
	}


	public void AddSystem(ISystem system)
	{
		_systems.Add(system);
		_logger.LogInformation("System {System} added", system);
	}


	public void AddEntity(Entity entity)
	{
		var componentTypes = GetComponentTypes(entity);

		var relevantSystems = _systems.Where(system => system.EntityIsMatch(componentTypes));

		foreach (var system in relevantSystems)
		{
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

		var relevantSystems =
			_systems
				.Where(system => !system.Contains(entity))
				.Where(system => system.EntityIsMatch(componentTypes));

		foreach (var system in relevantSystems)
		{
			system.Add(entity);
		}
	}


	public void RemoveComponent(Entity entity)
	{
		var componentTypes = GetComponentTypes(entity);

		var relevantSystems =
			_systems
				.Where(system => system.Contains(entity))
				.Where(system => !system.EntityIsMatch(componentTypes));

		foreach (var system in relevantSystems)
		{
			system.Remove(entity);
		}
	}


	private static HashSet<Type> GetComponentTypes(Entity entity) =>
		entity
			.GetComponents()
			.Select(x => x.GetType())
			.ToHashSet();
}
