using NGameEditor.Bridge.Scenes;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.Files;



public class AssetDescription(
	Guid id,
	string name,
	AssetTypeDefinition assetTypeDefinition,
	AbsolutePath path,
	List<PropertyDescription> properties
)
{
	public Guid Id { get; } = id;
	public string Name { get; } = name;
	public AssetTypeDefinition AssetTypeDefinition { get; } = assetTypeDefinition;
	public List<PropertyDescription> Properties { get; } = properties;
	public AbsolutePath Path { get; set; } = path;
}
