namespace NGame.Ecs;



public class EntityComponent
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public Entity? Entity;
}



[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ComponentAttribute : Attribute
{
	public string? Discriminator { get; set; }


	public static string GetDiscriminator(Type type) =>
		GetAttribute(type)?.Discriminator ?? type.FullName!;


	private static ComponentAttribute? GetAttribute(Type type) =>
		type
			.GetCustomAttributes(false)
			.Where(x => x is ComponentAttribute)
			.Cast<ComponentAttribute>()
			.FirstOrDefault();
}
