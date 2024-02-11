using System.IO.Compression;
using System.Text;
using NGame.Assets.Common.Assets;

namespace NGame.Platform.Assets.Unpacking;



public interface IAssetUnpacker
{
	string GetAssetJsonContent(Guid assetId);
	byte[]? GetCompanionFileBytes(Guid assetId);
}



public class AssetUnpacker(
	ITableOfContentsProvider tableOfContentsProvider,
	IAssetStreamProvider assetStreamProvider
) : IAssetUnpacker
{
	public string GetAssetJsonContent(Guid assetId)
	{
		var contentEntry = GetContentEntry(assetId);
		var assetPackPath = contentEntry.PackFileName;
		var filePath = contentEntry.FilePath;

		using var assetPackStream = assetStreamProvider.Open(assetPackPath);
		using var zipArchive = new ZipArchive(assetPackStream, ZipArchiveMode.Read);

		var zipArchiveEntry = zipArchive.GetEntry(filePath);
		if (zipArchiveEntry == null)
		{
			var message = $"Did not find {filePath} in {assetPackPath}";
			throw new InvalidOperationException(message);
		}

		using var zipStream = zipArchiveEntry.Open();
		using var reader = new StreamReader(zipStream, Encoding.UTF8);
		return reader.ReadToEnd();
	}


	public byte[]? GetCompanionFileBytes(Guid assetId)
	{
		var contentEntry = GetContentEntry(assetId);
		var assetPackPath = contentEntry.PackFileName;
		var filePath = contentEntry.FilePath;

		using var assetPackStream = assetStreamProvider.Open(assetPackPath);
		using var zipArchive = new ZipArchive(assetPackStream, ZipArchiveMode.Read);

		var companionFilePath = filePath[..^AssetConventions.AssetFileEnding.Length];
		var zipArchiveEntry = zipArchive.GetEntry(companionFilePath);

		if (zipArchiveEntry == null) return null;

		using var stream = zipArchiveEntry.Open();
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		return memoryStream.ToArray();
	}


	private ContentEntry GetContentEntry(Guid assetId)
	{
		var tableOfContents = tableOfContentsProvider.Get();
		var resourceIdentifiers = tableOfContents.ResourceIdentifiers;
		return resourceIdentifiers[assetId];
	}
}
