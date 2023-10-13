using NGame.Components;
using NGame.Services;

namespace NGame.Ecs;



public class EntityRegistry : IEntityRegistry
{
	private readonly IActionCache _actionCache;
	private readonly ISystemCollection _systemCollection;
	private readonly Dictionary<Entity, List<Component>> _components = new();


	public EntityRegistry(IActionCache actionCache, ISystemCollection systemCollection)
	{
		_actionCache = actionCache;
		_systemCollection = systemCollection;
	}


	public void AddEntity(Entity entity)
	{
		_actionCache.AddAction(() =>
		{
			_components.Add(entity, new List<Component>());
			_systemCollection.AddEntity(entity);
		});
	}


	public void RemoveEntity(Entity entity)
	{
		_actionCache.AddAction(() =>
		{
			_components.Remove(entity);
			_systemCollection.RemoveEntity(entity);
		});
	}


	public void AddComponent(Entity entity, Component component)
	{
		_actionCache.AddAction(() =>
		{
			_components[entity].Add(component);
			component.Entity = entity;
			_systemCollection.AddComponent(entity);
		});
	}


	public void RemoveComponent(Entity entity, Component component)
	{
		_actionCache.AddAction(() =>
		{
			_components[entity].Remove(component);
			_systemCollection.RemoveComponent(entity);
		});
	}


	public IReadOnlyList<Component> GetComponents(Entity entity) =>
		_components[entity];
}
