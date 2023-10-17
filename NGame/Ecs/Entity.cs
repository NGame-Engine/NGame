using NGame.Components;

namespace NGame.Ecs;



public sealed class Entity
{
	private readonly List<Component> _components = new();


	public Entity(IScene scene)
	{
		Transform = new Transform(this, scene);
	}


	public readonly Guid Id = Guid.NewGuid();
	public string Name { get; set; } = "Entity";
	public string Tag { get; set; } = "";


	public readonly Transform Transform;

	public IReadOnlyCollection<Component> Components => _components;


	public Entity AddComponent(Component component)
	{
		_components.Add(component);
		component.Entity = this;
		Transform.Scene.NotifyComponentAdded(this, component);
		return this;
	}


	public Entity RemoveComponent(Component component)
	{
		_components.Remove(component);
		component.Entity = null;
		Transform.Scene.NotifyComponentRemoved(this, component);
		return this;
	}
}



public static class EntityExtensions
{
	public static Entity CreateChildEntity(this Entity entity)
	{
		var childEntity = entity.Transform.Scene.CreateEntity();
		childEntity.Transform.SetParent(entity.Transform);
		return childEntity;
	}


	public static void Destroy(this Entity entity) =>
		entity.Transform.Scene.DestroyEntity(entity);


	public static T GetComponent<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.First();


	public static T? TryGetComponent<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.FirstOrDefault();


	public static IEnumerable<T> GetComponents<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>();


	public static IEnumerable<T> GetComponentsInChildren<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.Concat(
				entity
					.Transform
					.Children
					.Select(t => t.Entity)
					.SelectMany(t => t.GetComponentsInChildren<T>())
			);


	public static T GetRequiredSubTypeComponent<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType().IsAssignableTo(typeof(T)))
			.Cast<T>()
			.First();
}
