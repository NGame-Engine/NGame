namespace NGame.Assets;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ComponentAttribute : Attribute
{
	public string? StableDiscriminator { get; set; }
}
