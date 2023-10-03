using NGame.Ecs.Events;
using NGame.Scenes;
using NGame.UpdateSchedulers;

namespace NGame.Ecs;



internal class EventConnector
{
	private readonly ISceneEventBus _sceneEventBus;
	private readonly IEntityEventBus _entityEventBus;
	private readonly ISystemCollection _systemCollection;
	private readonly IFrameEventBus _frameEventBus;


	public EventConnector(
		ISceneEventBus sceneEventBus,
		IEntityEventBus entityEventBus,
		ISystemCollection systemCollection,
		IFrameEventBus frameEventBus
	)
	{
		_sceneEventBus = sceneEventBus;
		_entityEventBus = entityEventBus;
		_systemCollection = systemCollection;
		_frameEventBus = frameEventBus;
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
		_frameEventBus.DoAtNextFrameStart(
			() => _systemCollection.AddEntity(eventargs.Entity)
		);
	}


	private void OnEntityRemoved(Scene sender, EntityRemovedEventArgs eventargs)
	{
		_frameEventBus.DoAtNextFrameStart(
			() => _systemCollection.RemoveEntity(eventargs.Entity)
		);
	}


	private void OnComponentAdded(Entity sender, ComponentAddedEventArgs eventargs)
	{
		_frameEventBus.DoAtNextFrameStart(
			() => _systemCollection.AddComponent(sender)
		);
	}


	private void OnComponentRemoved(Entity sender, ComponentRemovedEventArgs eventargs)
	{
		_frameEventBus.DoAtNextFrameStart(
			() => _systemCollection.RemoveComponent(sender)
		);
	}
}
