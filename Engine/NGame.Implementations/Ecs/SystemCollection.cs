using NGame.Ecs;

namespace NGame.Implementations.Ecs;



public class SystemCollection : ISystemCollection
{
	private readonly IEnumerable<ISystem> _systems;


	public SystemCollection(IEnumerable<ISystem> systems)
	{
		_systems = systems;
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


	public void AddComponent(EntityComponent entityComponent)
	{
		var entity = entityComponent.Entity!;
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


	public void RemoveComponent(EntityComponent entityComponent)
	{
		var entity = entityComponent.Entity!;
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
		entity.Components
			.Select(x => x.GetType())
			.ToHashSet();
}
