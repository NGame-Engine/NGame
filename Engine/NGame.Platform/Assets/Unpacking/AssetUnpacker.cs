using System.IO.Compression;
using System.Text;
using NGame.Assets.Common.Assets;

namespace NGame.Platform.Assets.Unpacking;



public record RawAsset(string Json, byte[]? fileBytes);



public interface IAssetUnpacker
{
	RawAsset Unpack(Guid assetId);
}



public class AssetUnpacker(
	ITableOfContentsProvider tableOfContentsProvider,
	IAssetStreamProvider assetStreamProvider
) : IAssetUnpacker
{
	public RawAsset Unpack(Guid assetId)
	{
		var tableOfContents = tableOfContentsProvider.Get();
		var resourceIdentifiers = tableOfContents.ResourceIdentifiers;
		var contentEntry = resourceIdentifiers[assetId];
		var assetPackPath = contentEntry.PackFileName;
		var filePath = contentEntry.FilePath;

		using var assetPackStream = assetStreamProvider.Open(assetPackPath);
		using var zipArchive = new ZipArchive(assetPackStream, ZipArchiveMode.Read);

		var json = GetAssetJsonString(zipArchive, filePath, assetPackPath);
		var companionFileBytes = GetCompanionFileBytes(filePath, zipArchive);

		return new RawAsset(json, companionFileBytes);
	}


	private static string GetAssetJsonString(ZipArchive zipArchive, string filePath, string assetPackPath)
	{
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


	private static byte[]? GetCompanionFileBytes(string filePath, ZipArchive zipArchive)
	{
		var companionFilePath = filePath[..^AssetConventions.AssetFileEnding.Length];
		var zipArchiveEntry = zipArchive.GetEntry(companionFilePath);

		if (zipArchiveEntry == null) return null;

		using var stream = zipArchiveEntry.Open();
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		return memoryStream.ToArray();
	}
}
