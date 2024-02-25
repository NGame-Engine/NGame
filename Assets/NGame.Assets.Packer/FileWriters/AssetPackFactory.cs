using System.IO.Compression;
using System.Text;
using System.Text.Json;
using NGame.Assets.Common.Assets;
using NGame.Assets.Packer.AssetOverviews;
using NGame.Assets.Packer.Commands;
using Singulink.IO;

namespace NGame.Assets.Packer.FileWriters;



public interface IAssetPackFactory
{
	string Create(
		string packName,
		IEnumerable<AssetEntry> assetFileSpecifications,
		IAbsoluteDirectoryPath outputPath,
		ValidatedCommand validatedCommand
	);
}



public class AssetPackFactory : IAssetPackFactory
{
	public string Create(
		string packName,
		IEnumerable<AssetEntry> assetFileSpecifications,
		IAbsoluteDirectoryPath outputPath,
		ValidatedCommand validatedCommand
	)
	{
		var packFileName = $"{packName}{AssetConventions.PackFileEnding}";
		var absoluteFilePath = outputPath.CombineFile(packFileName);
		var zipFileName = absoluteFilePath.PathDisplay.ToLowerInvariant();

		using var fileStream = File.Open(zipFileName, FileMode.Create);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create);

		var minifyJson = validatedCommand.MinifyJson;

		foreach (var fileSpecification in assetFileSpecifications)
		{
			WriteFile(fileSpecification.MainPathInfo, zipArchive, minifyJson);
			if (fileSpecification.SatellitePathInfo == null) continue;

			WriteFile(fileSpecification.SatellitePathInfo, zipArchive, false);
		}

		return packFileName;
	}


	private static void WriteFile(PathInfo pathInfo, ZipArchive zipArchive, bool minifyJson)
	{
		using var sourceStream =
			minifyJson
				? OpenMinifiedJsonStream(pathInfo.SourcePath)
				: File.OpenRead(pathInfo.SourcePath.PathDisplay);

		var targetPath = pathInfo.GetNormalizedZipPath();
		var zipArchiveEntry = zipArchive.CreateEntry(targetPath, CompressionLevel.Fastest);
		using var zipStream = zipArchiveEntry.Open();
		sourceStream.CopyTo(zipStream);
	}


	private static Stream OpenMinifiedJsonStream(IAbsoluteFilePath absoluteFilePath)
	{
		var absolutePath = absoluteFilePath.PathDisplay;
		var allText = File.ReadAllText(absolutePath);
		var minified = JsonSerializer.Serialize(JsonSerializer.Deserialize<JsonDocument>(allText));
		var bytes = Encoding.UTF8.GetBytes(minified);
		return new MemoryStream(bytes);
	}
}
