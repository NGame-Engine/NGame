using System.IO.Compression;
using Microsoft.Extensions.Logging;
using NGame.Cli.PackAssetsCommand.Paths;
using NGame.Cli.PackAssetsCommand.Specifications;

namespace NGame.Cli.PackAssetsCommand.Writers;



public interface IAssetPackSpecificationWriter
{
	[Obsolete]
	void Write(
		AssetPackSpecification assetPackSpecification,
		AbsoluteNormalizedPath targetFolder
	);
}



public class AssetPackSpecificationWriter(ILogger<AssetPackSpecificationWriter> logger) : IAssetPackSpecificationWriter
{
	[Obsolete]
	public void Write(
		AssetPackSpecification assetPackSpecification,
		AbsoluteNormalizedPath targetFolder
	)
	{
		var packageName = assetPackSpecification.PackageName;
		var packFileName = Conventions.CreateAssetPackName(packageName);
		var absoluteFilePath = targetFolder.Combine(packFileName);

		using var fileStream = File.Open(absoluteFilePath.Value, FileMode.Create);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create);

		foreach (var fileSpecification in assetPackSpecification.FileSpecifications)
		{
			WriteEntry(zipArchive, fileSpecification);
		}


		logger.LogInformation("Wrote asset pack {Path}", absoluteFilePath);
	}


	private static void WriteEntry(ZipArchive zipArchive, AssetFileSpecification fileSpecification)
	{
		var absolutePath = fileSpecification.AbsolutePath.Value;
		var relativePath = fileSpecification.RelativePath.Value;

		using var assetFileStream = File.OpenRead(absolutePath);
		var zipArchiveEntry = zipArchive.CreateEntry(relativePath, CompressionLevel.Fastest);
		using var zipStream = zipArchiveEntry.Open();
		assetFileStream.CopyTo(zipStream);
	}
}
