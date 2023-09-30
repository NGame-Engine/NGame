namespace NGame.Assets;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ComponentAttribute : Attribute
{
	public string? StableDiscriminator { get; set; }
}
