namespace NGame.Ecs;

public static class EntityExtensions
{
	public static T? GetComponent<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.FirstOrDefault();


	public static T GetRequiredComponent<T>(this Entity entity) where T : Component =>
		entity
			.Components
			.Where(x => x.GetType() == typeof(T))
			.Cast<T>()
			.First();
}
