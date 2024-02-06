using NGame.Platform.Ecs.Implementations;
using NGame.Platform.Setup;
using NGame.Setup;

namespace NGame.Platform.Ecs;



public class EntityEventConnector : IBeforeApplicationStartListener
{
	private readonly IEntityEditor _entityEditor;
	private readonly IActionCache _actionCache;
	private readonly ISystemCollection _systemCollection;


	public EntityEventConnector(
		IEntityEditor entityEditor,
		IActionCache actionCache,
		ISystemCollection systemCollection
	)
	{
		_entityEditor = entityEditor;
		_actionCache = actionCache;
		_systemCollection = systemCollection;
	}


	public void OnBeforeApplicationStart()
	{
		_entityEditor.EntityCreated += e =>
			_actionCache.AddAction(() => _systemCollection.AddEntity(e));

		_entityEditor.EntityRemoving += e =>
			_actionCache.AddAction(() => _systemCollection.RemoveEntity(e));

		_entityEditor.ComponentAdded += c =>
			_actionCache.AddAction(() => _systemCollection.AddComponent(c));

		_entityEditor.ComponentRemoving += c =>
			_actionCache.AddAction(() => _systemCollection.RemoveComponent(c));
	}
}
