using System.Numerics;
using NGame.Scenes;

namespace NGame.Ecs;



public static class EntityExtensions
{
	public static Entity CreateEntityAt(this Scene scene, Vector3 position)
	{
		var entity = scene.CreateEntity();
		entity.Transform.Position = position;
		return entity;
	}


	public static T? GetComponent<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.FirstOrDefault();


	public static T GetRequiredComponent<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.First();


	public static T GetRequiredSubTypeComponent<T>(this Entity entity) where T : Component =>
		entity
			.GetComponents()
			.Where(x => x.GetType().IsAssignableTo(typeof(T)))
			.Cast<T>()
			.First();
}
