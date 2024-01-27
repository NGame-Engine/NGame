namespace NGame.Assets;



[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class AssetAttribute : Attribute
{
	public string? Name { get; set; }
	public string? Discriminator { get; set; }


	public static string GetName(Type type) =>
		GetAttribute(type)?.Name ?? type.Name;


	public static string GetDiscriminator(Type type) =>
		GetAttribute(type)?.Discriminator ?? type.FullName!;


	private static AssetAttribute? GetAttribute(Type type) =>
		type
			.GetCustomAttributes(false)
			.Where(x => x is AssetAttribute)
			.Cast<AssetAttribute>()
			.FirstOrDefault();
}
