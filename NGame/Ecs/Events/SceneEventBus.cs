using NGame.NGameSystem;
using NGame.Scenes;

namespace NGame.Ecs.Events;



public class ChildSceneAddedEventArgs
{
	public Scene Child;


	public ChildSceneAddedEventArgs(Scene child)
	{
		Child = child;
	}
}



public class ChildSceneRemovedEventArgs
{
	public Scene Child;


	public ChildSceneRemovedEventArgs(Scene child)
	{
		Child = child;
	}
}



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



public interface ISceneEventBus
{
	event GenericEventHandler<Scene, ChildSceneAddedEventArgs>? ChildSceneAdded;
	event GenericEventHandler<Scene, ChildSceneRemovedEventArgs>? ChildSceneRemoved;
	event GenericEventHandler<Scene, EntityAddedEventArgs>? EntityAdded;
	event GenericEventHandler<Scene, EntityRemovedEventArgs>? EntityRemoved;

	void NotifyChildSceneAdded(Scene scene, Scene child);
	void NotifyChildSceneRemoved(Scene scene, Scene child);

	void NotifyEntityAdded(Scene scene, Entity entity);
	void NotifyEntityRemoved(Scene scene, Entity entity);
}



internal class SceneEventBus : ISceneEventBus
{
	public event GenericEventHandler<Scene, ChildSceneAddedEventArgs>? ChildSceneAdded;
	public event GenericEventHandler<Scene, ChildSceneRemovedEventArgs>? ChildSceneRemoved;
	public event GenericEventHandler<Scene, EntityAddedEventArgs>? EntityAdded;
	public event GenericEventHandler<Scene, EntityRemovedEventArgs>? EntityRemoved;


	public void NotifyChildSceneAdded(Scene scene, Scene child)
	{
		var eventArgs = new ChildSceneAddedEventArgs(child);
		ChildSceneAdded?.Invoke(scene, eventArgs);
	}


	public void NotifyChildSceneRemoved(Scene scene, Scene child)
	{
		var eventArgs = new ChildSceneRemovedEventArgs(child);
		ChildSceneRemoved?.Invoke(scene, eventArgs);
	}


	public void NotifyEntityAdded(Scene scene, Entity entity)
	{
		var eventArgs = new EntityAddedEventArgs(entity);
		EntityAdded?.Invoke(scene, eventArgs);
	}


	public void NotifyEntityRemoved(Scene scene, Entity entity)
	{
		var eventArgs = new EntityRemovedEventArgs(entity);
		EntityRemoved?.Invoke(scene, eventArgs);
	}
}
