using NGame.Services;
using NGame.Systems;

namespace NGame.Ecs;



public interface IActionCache
{
	void OnEntityAdded( EntityAddedEventArgs eventArgs);
	void OnEntityRemoved( EntityRemovedEventArgs eventArgs);
	void OnComponentAdded(Entity sender, ComponentAddedEventArgs eventArgs);
	void OnComponentRemoved(Entity sender, ComponentRemovedEventArgs eventArgs);
}



public class ActionCache : IUpdatable, IActionCache
{
	private readonly ISystemCollection _systemCollection;
	private readonly List<Action> _frameStartActions = new();

	public int Order { get; set; } = -100000;


	public void Update(GameTime gameTime)
	{
		foreach (var action in _frameStartActions)
		{
			action();
		}

		_frameStartActions.Clear();
	}


	public ActionCache(ISystemCollection systemCollection)
	{
		_systemCollection = systemCollection;
	}


	public void OnEntityAdded( EntityAddedEventArgs eventArgs)
	{
		_frameStartActions.Add(
			() => _systemCollection.AddEntity(eventArgs.Entity)
		);
	}


	public void OnEntityRemoved( EntityRemovedEventArgs eventArgs)
	{
		_frameStartActions.Add(
			() => _systemCollection.RemoveEntity(eventArgs.Entity)
		);
	}


	public void OnComponentAdded(Entity sender, ComponentAddedEventArgs eventArgs)
	{
		_frameStartActions.Add(
			() => _systemCollection.AddComponent(sender)
		);
	}


	public void OnComponentRemoved(Entity sender, ComponentRemovedEventArgs eventArgs)
	{
		_frameStartActions.Add(
			() => _systemCollection.RemoveComponent(sender)
		);
	}
}
