using NGame.Ecs.Events;

namespace NGame.Ecs;



internal class EventConnector
{
	private readonly ISceneEventBus _sceneEventBus;
	private readonly IEntityEventBus _entityEventBus;
	private readonly IFrameStartCache _frameStartCache;


	public EventConnector(
		ISceneEventBus sceneEventBus,
		IEntityEventBus entityEventBus,
		IFrameStartCache frameStartCache
	)
	{
		_sceneEventBus = sceneEventBus;
		_entityEventBus = entityEventBus;
		_frameStartCache = frameStartCache;
	}


	public void ConnectEvents()
	{
		_sceneEventBus.EntityAdded += _frameStartCache.OnEntityAdded;
		_sceneEventBus.EntityRemoved += _frameStartCache.OnEntityRemoved;
		_entityEventBus.ComponentAdded += _frameStartCache.OnComponentAdded;
		_entityEventBus.ComponentRemoved += _frameStartCache.OnComponentRemoved;
	}
}
