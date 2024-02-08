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
		var contentEntry = resourceIdentifiers[mainAssetId];

		var type = asset.GetType();
		var processors = assetProcessors.Where(x => x.Type == type);


		using var assetPackStream = assetStreamProvider.Open(contentEntry.PackFileName);
		using var zipArchive = new ZipArchive(assetPackStream, ZipArchiveMode.Read);

		var filePath = contentEntry.FilePath;
		var companionFilePath = filePath[..^AssetConventions.AssetFileEnding.Length];
		var zipArchiveEntry = zipArchive.GetEntry(companionFilePath);

		var companionFileBytes = zipArchiveEntry == null
			? null
			: Load(zipArchiveEntry);

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Load(asset, companionFileBytes);
		}
	}


	private byte[] Load(ZipArchiveEntry zipArchiveEntry)
	{
		using var stream = zipArchiveEntry.Open();
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		return memoryStream.ToArray();
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
