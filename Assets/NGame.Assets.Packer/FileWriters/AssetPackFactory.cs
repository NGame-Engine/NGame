using System.IO.Compression;
using NGame.Assets.Common.Assets;
using NGame.Assets.Packer.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.Packer.FileWriters;



public interface IAssetPackFactory
{
	string Create(
		string packName,
		IEnumerable<AssetEntry> assetFileSpecifications,
		IAbsoluteDirectoryPath outputPath
	);
}



public class AssetPackFactory : IAssetPackFactory
{
	public string Create(
		string packName,
		IEnumerable<AssetEntry> assetFileSpecifications,
		IAbsoluteDirectoryPath outputPath
	)
	{
		var packFileName = $"{packName}{AssetConventions.PackFileEnding}";
		var absoluteFilePath = outputPath.CombineFile(packFileName);

		using var fileStream = File.Open(absoluteFilePath.PathDisplay, FileMode.Create);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create);

		foreach (var fileSpecification in assetFileSpecifications)
		{
			WriteEntry(zipArchive, fileSpecification.MainPathInfo);
			if (fileSpecification.SatellitePathInfo == null) continue;

			WriteEntry(zipArchive, fileSpecification.SatellitePathInfo);
		}

		return packFileName;
	}


	private static void WriteEntry(
		ZipArchive zipArchive,
		PathInfo fileReference
	)
	{
		var absolutePath = fileReference.AbsolutePath.PathDisplay;
		var relativePath = fileReference.RelativePath.PathDisplay;

		using var assetFileStream = File.OpenRead(absolutePath);
		var zipArchiveEntry = zipArchive.CreateEntry(relativePath, CompressionLevel.Fastest);
		using var zipStream = zipArchiveEntry.Open();
		assetFileStream.CopyTo(zipStream);
	}
}
