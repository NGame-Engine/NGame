using NGame.Ecs.Events;
using NGame.Scenes;
using NGame.UpdateSchedulers;

namespace NGame.Ecs;



public interface IFrameStartCache
{
	void OnEntityAdded(Scene sender, EntityAddedEventArgs eventArgs);
	void OnEntityRemoved(Scene sender, EntityRemovedEventArgs eventArgs);
	void OnComponentAdded(Entity sender, ComponentAddedEventArgs eventArgs);
	void OnComponentRemoved(Entity sender, ComponentRemovedEventArgs eventArgs);
}



public class FrameStartCache : IUpdatable, IFrameStartCache
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


	public FrameStartCache(ISystemCollection systemCollection)
	{
		_systemCollection = systemCollection;
	}


	public void OnEntityAdded(Scene sender, EntityAddedEventArgs eventArgs)
	{
		_frameStartActions.Add(
			() => _systemCollection.AddEntity(eventArgs.Entity)
		);
	}


	public void OnEntityRemoved(Scene sender, EntityRemovedEventArgs eventArgs)
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
