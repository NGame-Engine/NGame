using NGame.Components;

namespace NGame;


public class EntityAddedEventArgs
{
	public readonly Entity Entity;


	public EntityAddedEventArgs(Entity entity)
	{
		Entity = entity;
	}
}



public class EntityRemovedEventArgs
{
	public readonly Entity Entity;


	public EntityRemovedEventArgs(Entity entity)
	{
		Entity = entity;
	}
}



public class ComponentAddedEventArgs
{
	public readonly Component Component;


	public ComponentAddedEventArgs(Component component)
	{
		Component = component;
	}
}



public class ComponentRemovedEventArgs
{
	public readonly Component Component;


	public ComponentRemovedEventArgs(Component component)
	{
		Component = component;
	}
}



public interface IEntityRegistry
{
	event Action<EntityAddedEventArgs>? EntityAdded;
	event Action<EntityRemovedEventArgs>? EntityRemoved;
	// TODO single argument
	event Action<Entity, ComponentAddedEventArgs>? ComponentAdded;
	event Action<Entity, ComponentRemovedEventArgs>? ComponentRemoved;
	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);
	void AddComponent(Entity entity, Component component);
	void RemoveComponent(Entity entity, Component component);
	IReadOnlyList<Component> GetComponents(Entity entity);
}
