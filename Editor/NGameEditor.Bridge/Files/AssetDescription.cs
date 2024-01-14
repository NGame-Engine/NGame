using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Bridge.Files;



public class AssetDescription(
	Guid id,
	string name,
	AssetTypeDefinition assetTypeDefinition,
	List<PropertyDescription> properties
)
{
	public Guid Id { get; } = id;
	public string Name { get; } = name;
	public AssetTypeDefinition AssetTypeDefinition { get; } = assetTypeDefinition;
	public List<PropertyDescription> Properties { get; } = properties;
}
