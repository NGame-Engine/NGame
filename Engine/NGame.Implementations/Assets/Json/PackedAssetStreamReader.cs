using System.IO.Compression;
using NGame.Assets;
using NGame.Core.Assets.ContentTables;

namespace NGame.Core.Assets.Json;



public class PackedAssetStreamReader : IPackedAssetStreamReader
{
	private readonly ITableOfContentsProvider _tableOfContentsProvider;
	private readonly IAssetStreamProvider _assetStreamProvider;


	public PackedAssetStreamReader(
		ITableOfContentsProvider tableOfContentsProvider,
		IAssetStreamProvider assetStreamProvider
	)
	{
		_tableOfContentsProvider = tableOfContentsProvider;
		_assetStreamProvider = assetStreamProvider;
	}


	public T ReadFromStream<T>(AssetId assetId, Func<Stream, T> useStream)
	{
		var tableOfContents = _tableOfContentsProvider.Get();
		var contentEntry = tableOfContents.ResourceIdentifiers[assetId.Id];
		var assetPackPath = contentEntry.PackFileName;
		var pathInFile = contentEntry.FilePath;

		using var fileStream = _assetStreamProvider.Open(assetPackPath);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read);


		var zipArchiveEntry = zipArchive.GetEntry(pathInFile)!;
		if (zipArchiveEntry == null)
		{
			var message = $"Did not find {pathInFile} in {assetPackPath}";
			throw new InvalidOperationException(message);
		}

		using var zipStream = zipArchiveEntry.Open();
		return useStream(zipStream);
	}
}
