namespace NGame.Ecs;



internal class EventConnector
{
	private readonly IEntityRegistry _entityRegistry;
	private readonly IActionCache _actionCache;


	public EventConnector(IEntityRegistry entityRegistry, IActionCache actionCache)
	{
		_entityRegistry = entityRegistry;
		_actionCache = actionCache;
	}


	public void ConnectEvents()
	{
		_entityRegistry.EntityAdded += _actionCache.OnEntityAdded;
		_entityRegistry.EntityRemoved += _actionCache.OnEntityRemoved;
		_entityRegistry.ComponentAdded += _actionCache.OnComponentAdded;
		_entityRegistry.ComponentRemoved += _actionCache.OnComponentRemoved;
	}
}
