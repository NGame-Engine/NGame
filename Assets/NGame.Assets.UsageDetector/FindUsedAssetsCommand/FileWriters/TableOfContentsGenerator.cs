using NGame.Tooling.Assets;

namespace NGame.Cli.FindUsedAssetsCommand.FileWriters;



public interface ITableOfContentsGenerator
{
	TableOfContents CreateTableOfContents(IEnumerable<AssetFileSpecification> assetEntries);
}



public class TableOfContentsGenerator : ITableOfContentsGenerator
{


	public TableOfContents CreateTableOfContents(
		IEnumerable<AssetFileSpecification> assetFileSpecifications
		) =>
		new()
		{
			ResourceIdentifiers =
				assetFileSpecifications
					.ToDictionary(
						x => x.Id,
						x => new ContentEntry
						{
							PackFileName = $"{x.PackageName}{AssetConventions.PackFileEnding}",
							FilePath = x.AssetFile.RelativePath.PathDisplay
						}
					)
		};
}
