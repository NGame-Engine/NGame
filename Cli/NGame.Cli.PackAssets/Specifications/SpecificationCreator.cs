using NGame.Cli.Abstractions.Paths;
using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.CommandValidators;

namespace NGame.Cli.PackAssets.Specifications;



public interface ISpecificationCreator
{
	ICollection<AssetPackSpecification> CreateSpecifications(
		ValidatedCommand validatedCommand,
		IEnumerable<AssetFileEntry> assetFileEntries
	);
}



public class SpecificationCreator : ISpecificationCreator
{
	public ICollection<AssetPackSpecification> CreateSpecifications(
		ValidatedCommand validatedCommand,
		IEnumerable<AssetFileEntry> assetFileEntries
	)
	{
		var projectFolder = validatedCommand.ProjectFolder;

		return
			assetFileEntries
				.GroupBy(x => x.PackageName)
				.Select(x => CreatePackSpecification(x.Key, x, projectFolder))
				.ToList();
	}

	[Obsolete]
	private static AssetPackSpecification CreatePackSpecification(
		PackageName packageName,
		IEnumerable<AssetFileEntry> assetFileEntries,
		AbsoluteNormalizedPath projectFolder
	) =>
		new(
			packageName,
			assetFileEntries
				.Select(x => CreateFileSpecification(x, projectFolder))
				.ToList()
		);

	[Obsolete]
	private static AssetFileSpecification CreateFileSpecification(
		AssetFileEntry assetFileEntry,
		AbsoluteNormalizedPath projectFolder
	)
	{
		var relativeFilePath = assetFileEntry.FilePath;
		var absolutePath = projectFolder.Combine(relativeFilePath.Value);
		return new AssetFileSpecification(absolutePath, relativeFilePath);
	}
}
