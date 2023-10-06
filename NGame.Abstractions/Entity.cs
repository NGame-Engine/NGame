using NGame.Assets;
using NGame.Components;

namespace NGame;



public sealed class Entity
{
	public Entity(Scene scene)
	{
		Scene = scene;
		Transform = new Transform(this);
	}


	// TODO remove internal set before moving to abstractions
	public Scene Scene { get; internal set; }

	public readonly Guid Id = Guid.NewGuid();
	public string Name { get; set; } = "Entity";
	public string Tag { get; set; } = "";


	public readonly Transform Transform;
}



public static class EntityExtensions
{
	public static Entity CreateChildEntity(this Entity entity)
	{
		var childEntity = entity.Scene.CreateEntity();
		childEntity.Transform.SetParent(entity.Transform);
		return childEntity;
	}


	public static void Destroy(this Entity entity) =>
		entity.Scene.Destroy(entity);


	public static Entity AddComponent(this Entity entity, Component component)
	{
		entity.Scene.EntityRegistry.AddComponent(entity, component);
		return entity;
	}


	public static Entity RemoveComponent(this Entity entity, Component component)
	{
		entity.Scene.EntityRegistry.RemoveComponent(entity, component);
		return entity;
	}


	public static IEnumerable<Component> GetComponents(this Entity entity) =>
		entity
			.Scene.EntityRegistry
			.GetComponents(entity);


	public static T GetComponent<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.First();


	public static T? TryGetComponent<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.FirstOrDefault();


	public static IEnumerable<T> GetComponents<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>();


	public static IEnumerable<T> GetComponentsInChildren<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
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
			.GetComponents()
			.Where(x => x.GetType().IsAssignableTo(typeof(T)))
			.Cast<T>()
			.First();
}
