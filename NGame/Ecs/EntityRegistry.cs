using NGame.Components;

namespace NGame.Ecs;




public class EntityRegistry : IEntityRegistry
{
	private readonly Dictionary<Entity, List<Component>> _components = new();

	public event Action<EntityAddedEventArgs>? EntityAdded;
	public event Action<EntityRemovedEventArgs>? EntityRemoved;
	public event Action<Entity, ComponentAddedEventArgs>? ComponentAdded;
	public event Action<Entity, ComponentRemovedEventArgs>? ComponentRemoved;


	public void AddEntity(Entity entity)
	{
		_components.Add(entity, new List<Component>());
		var eventArgs = new EntityAddedEventArgs(entity);
		EntityAdded?.Invoke(eventArgs);
	}


	public void RemoveEntity(Entity entity)
	{
		_components.Remove(entity);
		var eventArgs = new EntityRemovedEventArgs(entity);
		EntityRemoved?.Invoke(eventArgs);
	}


	public void AddComponent(Entity entity, Component component)
	{
		_components[entity].Add(component);
		component.Entity = entity;
		var eventArgs = new ComponentAddedEventArgs(component);
		ComponentAdded?.Invoke(entity, eventArgs);
	}


	public void RemoveComponent(Entity entity, Component component)
	{
		_components[entity].Remove(component);
		var eventArgs = new ComponentRemovedEventArgs(component);
		ComponentRemoved?.Invoke(entity, eventArgs);
	}


	public IReadOnlyList<Component> GetComponents(Entity entity) =>
		_components[entity];
}
