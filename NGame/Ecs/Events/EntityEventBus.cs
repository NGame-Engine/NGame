using NGame.NGameSystem;

namespace NGame.Ecs.Events;



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



public interface IEntityEventBus
{
	event GenericEventHandler<Entity, ComponentAddedEventArgs>? ComponentAdded;
	event GenericEventHandler<Entity, ComponentRemovedEventArgs>? ComponentRemoved;


	void NotifyComponentAdded(Entity entity, Component component);
	void NotifyComponentRemoved(Entity entity, Component component);
}



internal class EntityEventBus : IEntityEventBus
{
	public event GenericEventHandler<Entity, ComponentAddedEventArgs>? ComponentAdded;
	public event GenericEventHandler<Entity, ComponentRemovedEventArgs>? ComponentRemoved;


	public void NotifyComponentAdded(Entity entity, Component component)
	{
		var eventArgs = new ComponentAddedEventArgs(component);
		ComponentAdded?.Invoke(entity, eventArgs);
	}


	public void NotifyComponentRemoved(Entity entity, Component component)
	{
		var eventArgs = new ComponentRemovedEventArgs(component);
		ComponentRemoved?.Invoke(entity, eventArgs);
	}


	// TODO Queue a transform update
	public void NotifyEntityAddedToScene()
	{
	}
}
