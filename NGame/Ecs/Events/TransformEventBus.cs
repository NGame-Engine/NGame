using NGame.Components.Transforms;
using NGame.NGameSystem;

namespace NGame.Ecs.Events;



public class ChildrenCollectionChangedEventArgs
{
	internal readonly Transform Transform;
	internal readonly bool Added;


	public ChildrenCollectionChangedEventArgs(Transform transform, bool added)
	{
		this.Transform = transform;
		this.Added = added;
	}
}



public interface ITransformEventBus
{
	event GenericEventHandler<Transform, ChildrenCollectionChangedEventArgs>? ChildrenCollectionChanged;

	void NotifyChildrenCollectionChanged(Transform transform, bool added);
}



internal class TransformEventBus : ITransformEventBus
{
	public event GenericEventHandler<Entity, ComponentAddedEventArgs>? ComponentAdded;
	public event GenericEventHandler<Entity, ComponentRemovedEventArgs>? ComponentRemoved;
	public event GenericEventHandler<Transform, ChildrenCollectionChangedEventArgs>? ChildrenCollectionChanged;


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


	public void NotifyChildrenCollectionChanged(Transform transform, bool added)
	{
		var eventArgs = new ChildrenCollectionChangedEventArgs(transform, added);
		ChildrenCollectionChanged?.Invoke(transform, eventArgs);
	}


	// TODO Queue a transform update
	public void NotifyEntityAddedToScene()
	{
	}
}
