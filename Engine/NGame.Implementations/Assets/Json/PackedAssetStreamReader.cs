using System.IO.Compression;
using NGame.Assets;
using NGame.Implementations.Assets.ContentTables;
using NGame.Implementations.Assets.Readers;
using NGame.Implementations.Assets.Registries;

namespace NGame.Implementations.Assets.Json;



public class PackedAssetStreamReader(
	ITableOfContentsProvider tableOfContentsProvider,
	IAssetStreamProvider assetStreamProvider
)
	: IPackedAssetStreamReader
{
	public T ReadFromStream<T>(AssetId assetId, Func<Stream, T> useStream)
	{
		var tableOfContents = tableOfContentsProvider.Get();
		var contentEntry = tableOfContents.ResourceIdentifiers[assetId.Id];
		var assetPackPath = contentEntry.PackFileName;
		var pathInFile = contentEntry.FilePath;

		using var fileStream = assetStreamProvider.Open(assetPackPath);
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
