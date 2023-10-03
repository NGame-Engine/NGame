using NGame.Components.Transforms;
using NGame.Ecs.Events;
using NGame.Scenes;

namespace NGame.Ecs;



public sealed class Entity
{
	private readonly IEcsTypeFactory _ecsTypeFactory;
	private readonly IEntityEventBus _entityEventBus;
	private readonly List<Component> _components = new();


	public Entity(
		Scene scene,
		IEntityEventBus entityEventBus,
		IEcsTypeFactory ecsTypeFactory
	)
	{
		_ecsTypeFactory = ecsTypeFactory;
		_entityEventBus = entityEventBus;
		Scene = scene;
		Transform = new Transform(this);
	}


	public Scene Scene { get; internal set; }

	public readonly Guid Id = Guid.NewGuid();
	public string Name { get; set; } = "Entity";


	public readonly Transform Transform;


	public Entity CreateChildEntity()
	{
		var child = new Entity(Scene, _entityEventBus, _ecsTypeFactory);
		child.Transform.SetParent(Transform);
		return child;
	}


	public T AddComponent<T>() where T : Component
	{
		var component = _ecsTypeFactory.CreateComponent<T>();
		_components.Add(component);
		component.Entity = this;

		_entityEventBus.NotifyComponentAdded(this, component);

		return component;
	}


	public void RemoveComponent(Component component)
	{
		_components.Remove(component);
		_entityEventBus.NotifyComponentRemoved(this, component);
	}


	public IReadOnlyList<Component> GetComponents() => _components;
}
