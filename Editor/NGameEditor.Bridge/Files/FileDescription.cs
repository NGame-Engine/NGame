namespace NGameEditor.Bridge.Files;



public class FileDescription(
	string name,
	AssetTypeDefinition? assetTypeDefinition
)
{
	public string Name { get; } = name;
	public AssetTypeDefinition? AssetTypeDefinition { get; } = assetTypeDefinition;
}
