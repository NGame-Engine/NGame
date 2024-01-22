using NGame.Assets;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



internal interface IAssetFileWatcher
{
	Result<IEnumerable<AssetDescription>> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition);
	Result<IEnumerable<AssetDescription>> GetAssetsOfType(string typeIdentifier);
}



internal class AssetFileWatcher(
	IEnumerable<AssetDescription> initialFiles,
	IAssetDescriptionReader assetDescriptionReader
) : IAssetFileWatcher
{
	private List<AssetDescription> AssetDescriptions { get; } = new(initialFiles);


	private static bool IsAssertFilePath(AbsolutePath absolutePath) =>
		absolutePath.Path.EndsWith(AssetConventions.AssetFileEnding);


	public Result<IEnumerable<AssetDescription>> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition)
	{
		var assetDescriptions =
			AssetDescriptions
				.Where(x => x.AssetTypeDefinition.Identifier == assetTypeDefinition.Identifier);

		return Result.Success(assetDescriptions);
	}


	public Result<IEnumerable<AssetDescription>> GetAssetsOfType(string typeIdentifier)
	{
		var assetDescriptions =
			AssetDescriptions
				.Where(x => x.AssetTypeDefinition.Identifier == typeIdentifier);

		return Result.Success(assetDescriptions);
	}


	public void OnChanged(FileChangedArgs args)
	{
		if (IsAssertFilePath(args.Path) == false) return;

		AssetDescriptions
			.RemoveAll(x => x.FilePath == args.Path);

		var assetDescription = assetDescriptionReader.ReadAsset(args.Path);
		AssetDescriptions.Add(assetDescription);
	}


	public void OnCreated(FileCreatedArgs args)
	{
		if (IsAssertFilePath(args.Path) == false) return;

		var assetDescription = assetDescriptionReader.ReadAsset(args.Path);
		AssetDescriptions.Add(assetDescription);
	}


	public void OnDeleted(FileDeletedArgs args)
	{
		if (IsAssertFilePath(args.Path) == false) return;

		AssetDescriptions
			.RemoveAll(x => x.FilePath == args.Path);
	}


	public void OnRenamed(FileRenamedArgs args)
	{
		var oldPathIsAssetFilePath = IsAssertFilePath(args.OldPath);
		var newPathIsAssetFilePath = IsAssertFilePath(args.Path);


		if (oldPathIsAssetFilePath)
		{
			AssetDescriptions
				.RemoveAll(x => x.FilePath == args.Path);
		}

		if (newPathIsAssetFilePath)
		{
			var assetDescription = assetDescriptionReader.ReadAsset(args.Path);
			AssetDescriptions.Add(assetDescription);
		}
	}
}
