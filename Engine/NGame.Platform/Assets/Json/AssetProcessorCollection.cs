using System.IO.Compression;
using NGame.Assets;
using NGame.Assets.Common.Assets;
using NGame.Platform.Assets.ContentTables;
using NGame.Platform.Assets.Readers;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets.Json;



public interface IAssetProcessorCollection
{
	void Load(Asset asset);
	void Unload(Asset asset);
}



public class AssetProcessorCollection(
	IEnumerable<IAssetProcessor> assetProcessors,
	ITableOfContentsProvider tableOfContentsProvider,
	IAssetStreamProvider assetStreamProvider
)
	: IAssetProcessorCollection
{
	public void Load(Asset asset)
	{
		var tableOfContents = tableOfContentsProvider.Get();
		var resourceIdentifiers = tableOfContents.ResourceIdentifiers;

		var mainAssetId = asset.Id;
		var contentEntry = resourceIdentifiers[mainAssetId.Id];

		var type = asset.GetType();
		var processors = assetProcessors.Where(x => x.Type == type);


		using var assetPackStream = assetStreamProvider.Open(contentEntry.PackFileName);
		using var zipArchive = new ZipArchive(assetPackStream, ZipArchiveMode.Read);

		var filePath = contentEntry.FilePath;
		var companionFilePath = filePath[..^AssetConventions.AssetFileEnding.Length];
		var zipArchiveEntry = zipArchive.GetEntry(companionFilePath)!;

		foreach (var assetProcessor in processors)
		{
			using var assetStream = zipArchiveEntry.Open();
			var assetStreamReader = new AssetStreamReader(asset.Id, companionFilePath, zipArchiveEntry.Open);
			assetProcessor.Load(asset, assetStreamReader);
		}
	}


	public void Unload(Asset asset)
	{
		var processors = assetProcessors.Where(x => x.Type == asset.GetType());

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Unload(asset);
		}
	}
}
