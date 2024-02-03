using NGame.Assets;
using NGame.Tooling.Assets;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



internal interface IAssetFileWatcher
{
	IEnumerable<AssetDescription> GetAssetDescriptions();
	Result<AssetDescription> GetById(Guid id);
}



internal class AssetFileWatcher(
	IEnumerable<AssetDescription> initialFiles,
	IAssetDescriptionReader assetDescriptionReader
) : IAssetFileWatcher
{
	private List<AssetDescription> AssetDescriptions { get; } = [.. initialFiles];


	private static bool IsAssetFilePath(AbsolutePath absolutePath) =>
		absolutePath.Path.EndsWith(AssetConventions.AssetFileEnding);


	public IEnumerable<AssetDescription> GetAssetDescriptions() => AssetDescriptions;


	public Result<AssetDescription> GetById(Guid id) =>
		AssetDescriptions
			.Where(x => x.Id == id)
			.Select(Result.Success)
			.FirstOrDefault()
		?? Result.Error($"Unable to find asset with ID {id}");


	public void OnChanged(FileChangedArgs args)
	{
		if (IsAssetFilePath(args.Path) == false) return;

		AssetDescriptions
			.RemoveAll(x => x.FilePath == args.Path);

		var assetDescription = assetDescriptionReader.ReadAsset(args.Path);
		AssetDescriptions.Add(assetDescription);
	}


	public void OnCreated(FileCreatedArgs args)
	{
		if (IsAssetFilePath(args.Path) == false) return;

		var assetDescription = assetDescriptionReader.ReadAsset(args.Path);
		AssetDescriptions.Add(assetDescription);
	}


	public void OnDeleted(FileDeletedArgs args)
	{
		if (IsAssetFilePath(args.Path) == false) return;

		AssetDescriptions
			.RemoveAll(x => x.FilePath == args.Path);
	}


	public void OnRenamed(FileRenamedArgs args)
	{
		var oldPathIsAssetFilePath = IsAssetFilePath(args.OldPath);
		var newPathIsAssetFilePath = IsAssetFilePath(args.Path);


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
