using NGameEditor.Bridge.Shared;

namespace NGameEditor.Bridge.Files;



public class AssetDescription(
	Guid id,
	string name,
	AssetTypeDefinition assetTypeDefinition,
	AbsolutePath filePath
)
{
	public Guid Id { get; } = id;
	public string Name { get; } = name;
	public AssetTypeDefinition AssetTypeDefinition { get; } = assetTypeDefinition;
	public AbsolutePath FilePath { get; } = filePath;
}
