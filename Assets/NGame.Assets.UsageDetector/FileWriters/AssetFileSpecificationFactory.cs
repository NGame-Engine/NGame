using NGame.Assets.UsageDetector.AssetOverviews;
using Singulink.IO;

namespace NGame.Assets.UsageDetector.FileWriters;



public interface IAssetFileSpecificationFactory
{
	AssetFileSpecification Create(
		AssetEntry assetEntry,
		IAbsoluteDirectoryPath projectFolder
	);
}



public class AssetFileSpecificationFactory : IAssetFileSpecificationFactory
{
	public AssetFileSpecification Create(
		AssetEntry assetEntry,
		IAbsoluteDirectoryPath projectFolder
	)
	{
		var absolutePath = assetEntry.FilePath;
		var assetFileReference = GetFileReference(absolutePath, projectFolder);

		var companionFileReference = assetEntry.CompanionFile == null
			? null
			: GetFileReference(assetEntry.CompanionFile, projectFolder);

		return new AssetFileSpecification(
			assetEntry.Id,
			assetEntry.PackageName,
			assetFileReference,
			companionFileReference
		);
	}


	private static FileReference GetFileReference(
		IAbsoluteFilePath absoluteFilePath,
		IAbsoluteDirectoryPath relativeTo
	)
	{
		var relativePathString = Path.GetRelativePath(
			relativeTo.PathDisplay,
			absoluteFilePath.PathDisplay
		);
		var relativeFilePath = FilePath.ParseRelative(relativePathString);
		return new FileReference(absoluteFilePath, relativeFilePath);
	}
}
