using System.IO.Compression;
using NGame.Assets;
using NGame.Core.Assets.ContentTables;

namespace NGame.Core.Assets.Json;



public interface IAssetProcessorCollection
{
	void Load(Asset asset);
	void Unload(Asset asset);
}



public class AssetProcessorCollection : IAssetProcessorCollection
{
	private readonly IEnumerable<IAssetProcessor> _assetProcessors;
	private readonly ITableOfContentsProvider _tableOfContentsProvider;
	private readonly IAssetStreamProvider _assetStreamProvider;


	public AssetProcessorCollection(
		IEnumerable<IAssetProcessor> assetProcessors,
		ITableOfContentsProvider tableOfContentsProvider,
		IAssetStreamProvider assetStreamProvider
	)
	{
		_assetProcessors = assetProcessors;
		_tableOfContentsProvider = tableOfContentsProvider;
		_assetStreamProvider = assetStreamProvider;
	}


	public void Load(Asset asset)
	{
		var tableOfContents = _tableOfContentsProvider.Get();
		var resourceIdentifiers = tableOfContents.ResourceIdentifiers;

		var mainAssetId = asset.Id;
		var contentEntry = resourceIdentifiers[mainAssetId];

		var type = asset.GetType();
		var processors = _assetProcessors.Where(x => x.Type == type);


		using var assetPackStream = _assetStreamProvider.Open(contentEntry.PackFileName);
		using var zipArchive = new ZipArchive(assetPackStream, ZipArchiveMode.Read);

		var filePath = contentEntry.FilePath;
		var companionFilePath = filePath[..^AssetConventions.AssetFileEnding.Length];
		var zipArchiveEntry = zipArchive.GetEntry(companionFilePath)!;

		foreach (var assetProcessor in processors)
		{
			var openStream = () => zipArchiveEntry.Open();
			using var assetStream = zipArchiveEntry.Open();
			var assetStreamReader = new AssetStreamReader(asset.Id, companionFilePath, openStream);
			assetProcessor.Load(asset, assetStreamReader);
		}
	}


	public void Unload(Asset asset)
	{
		var processors = _assetProcessors.Where(x => x.Type == asset.GetType());

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Unload(asset);
		}
	}
}
