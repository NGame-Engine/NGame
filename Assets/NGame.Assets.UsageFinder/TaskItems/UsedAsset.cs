using Singulink.IO;

namespace NGame.Assets.UsageFinder.TaskItems;



public class UsedAsset(
	IAbsoluteFilePath sourcePath,
	Guid assetId,
	string package,
	IAbsoluteDirectoryPath projectDirectory,
	IRelativeFilePath jsonFilePath,
	IRelativeFilePath? dataFilePath
)
{
	public IAbsoluteFilePath SourcePath { get; } = sourcePath;
	public Guid AssetId { get; } = assetId;
	public string Package { get; } = package;
	public IAbsoluteDirectoryPath ProjectDirectory { get; } = projectDirectory;
	public IRelativeFilePath JsonFilePath { get; } = jsonFilePath;
	public IRelativeFilePath? DataFilePath { get; } = dataFilePath;
}
