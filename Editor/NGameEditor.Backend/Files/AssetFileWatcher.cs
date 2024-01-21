using NGame.Assets;
using NGameEditor.Backend.Scenes;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



internal interface IAssetFileWatcher
{
	Result<IEnumerable<AssetDescription>> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition);
}



internal class AssetFileWatcher(
	ISceneFileIdReader sceneFileIdReader,
	List<AssetDescription> initialFiles,
	IAssetDeserializerOptionsFactory assetDeserializerOptionsFactory,
	IBackendAssetDeserializer backendAssetDeserializer
) : IAssetFileWatcher
{
	private List<AssetDescription> AssetDescriptions { get; } = initialFiles;


	private static bool IsAssertFilePath(AbsolutePath absolutePath) =>
		absolutePath.Path.EndsWith(AssetConventions.AssetFileEnding);


	public Result<IEnumerable<AssetDescription>> GetAssetsOfType(AssetTypeDefinition assetTypeDefinition)
	{
		var assetDescriptions =
			AssetDescriptions
				.Where(x => x.AssetTypeDefinition.Identifier == assetTypeDefinition.Identifier);

		return Result.Success(assetDescriptions);
	}


	public void OnChanged(FileChangedArgs args)
	{
		if (IsAssertFilePath(args.Path) == false) return;

		AssetDescriptions
			.RemoveAll(x => x.Path == args.Path);

		var assetDescription = ReadAsset(args.Path);
		AssetDescriptions.Add(assetDescription);
	}


	public void OnCreated(FileCreatedArgs args)
	{
		if (IsAssertFilePath(args.Path) == false) return;

		var assetDescription = ReadAsset(args.Path);
		AssetDescriptions.Add(assetDescription);
	}


	public void OnDeleted(FileDeletedArgs args)
	{
		if (IsAssertFilePath(args.Path) == false) return;

		AssetDescriptions
			.RemoveAll(x => x.Path == args.Path);
	}


	public void OnRenamed(FileRenamedArgs args)
	{
		var oldPathIsAssetFilePath = IsAssertFilePath(args.OldPath);
		var newPathIsAssetFilePath = IsAssertFilePath(args.Path);


		if (oldPathIsAssetFilePath)
		{
			AssetDescriptions
				.RemoveAll(x => x.Path == args.Path);
		}

		if (newPathIsAssetFilePath)
		{
			var assetDescription = ReadAsset(args.Path);
			AssetDescriptions.Add(assetDescription);
		}
	}


	private AssetDescription ReadAsset(AbsolutePath absolutePath)
	{
		var readAssetResult = backendAssetDeserializer.ReadAsset(absolutePath);
	}
}
