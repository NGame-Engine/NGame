using NGame.Ecs.Events;
using NGame.Scenes;

namespace NGame.Ecs;



internal class EventConnector
{
	private readonly ISceneEventBus _sceneEventBus;
	private readonly IEntityEventBus _entityEventBus;
	private readonly ISystemCollection _systemCollection;


	public EventConnector(
		ISceneEventBus sceneEventBus,
		IEntityEventBus entityEventBus,
		ISystemCollection systemCollection
	)
	{
		_sceneEventBus = sceneEventBus;
		_entityEventBus = entityEventBus;
		_systemCollection = systemCollection;
	}


	public void ConnectEvents()
	{
		_sceneEventBus.EntityAdded += OnEntityAdded;
		_sceneEventBus.EntityRemoved += OnEntityRemoved;
		_entityEventBus.ComponentAdded += OnComponentAdded;
		_entityEventBus.ComponentRemoved += OnComponentRemoved;
	}


	private void OnEntityAdded(Scene sender, EntityAddedEventArgs eventargs)
	{
		_systemCollection.AddEntity(eventargs.Entity);
	}


	private void OnEntityRemoved(Scene sender, EntityRemovedEventArgs eventargs)
	{
		_systemCollection.RemoveEntity(eventargs.Entity);
	}


	private void OnComponentAdded(Entity sender, ComponentAddedEventArgs eventargs)
	{
		_systemCollection.AddComponent(sender);
	}


	private void OnComponentRemoved(Entity sender, ComponentRemovedEventArgs eventargs)
	{
		_systemCollection.RemoveComponent(sender);
	}
}
