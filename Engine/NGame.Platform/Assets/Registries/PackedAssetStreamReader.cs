using System.IO.Compression;
using System.Text;
using NGame.Platform.Assets.ContentTables;
using NGame.Platform.Assets.Readers;

namespace NGame.Platform.Assets.Registries;



public interface IPackedAssetStreamReader
{
	string ReadFromStream(Guid assetId);
}



public class PackedAssetStreamReader(
	ITableOfContentsProvider tableOfContentsProvider,
	IAssetStreamProvider assetStreamProvider
)
	: IPackedAssetStreamReader
{
	public string ReadFromStream(Guid assetId)
	{
		var tableOfContents = tableOfContentsProvider.Get();
		var contentEntry = tableOfContents.ResourceIdentifiers[assetId];
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
		using var reader = new StreamReader(zipStream, Encoding.UTF8);
		return reader.ReadToEnd();
	}
}
