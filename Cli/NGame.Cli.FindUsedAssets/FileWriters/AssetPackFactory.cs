using System.IO.Compression;
using NGame.Assets;
using Singulink.IO;

namespace NGame.Cli.FindUsedAssets.FileWriters;



public interface IAssetPackFactory
{
	string Create(
		string packName,
		IEnumerable<AssetFileSpecification> assetFileSpecifications,
		IAbsoluteDirectoryPath outputPath
	);
}



public class AssetPackFactory : IAssetPackFactory
{
	public string Create(
		string packName,
		IEnumerable<AssetFileSpecification> assetFileSpecifications,
		IAbsoluteDirectoryPath outputPath
	)
	{
		var packFileName = $"{packName}{AssetConventions.PackFileEnding}";
		var absoluteFilePath = outputPath.CombineFile(packFileName);

		using var fileStream = File.Open(absoluteFilePath.PathExport, FileMode.Create);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create);

		foreach (var fileSpecification in assetFileSpecifications)
		{
			WriteEntry(zipArchive, fileSpecification.AssetFile);
			if (fileSpecification.CompanionFile == null) continue;

			WriteEntry(zipArchive, fileSpecification.CompanionFile);
		}

		return packFileName;
	}


	private static void WriteEntry(
		ZipArchive zipArchive,
		FileReference fileReference
	)
	{
		var absolutePath = fileReference.AbsolutePath.PathExport;
		var relativePath = fileReference.RelativePath.PathDisplay;

		using var assetFileStream = File.OpenRead(absolutePath);
		var zipArchiveEntry = zipArchive.CreateEntry(relativePath, CompressionLevel.Fastest);
		using var zipStream = zipArchiveEntry.Open();
		assetFileStream.CopyTo(zipStream);
	}
}
