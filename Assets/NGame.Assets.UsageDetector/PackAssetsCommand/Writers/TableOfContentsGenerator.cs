using NGame.Assets;
using NGame.Cli.PackAssetsCommand.AssetFileReaders;
using NGame.Cli.PackAssetsCommand.Paths;
using NGame.Cli.PackAssetsCommand.Specifications;
using NGame.Tooling.Assets;

namespace NGame.Cli.PackAssetsCommand.Writers;



public interface ITableOfContentsGenerator
{
	TableOfContents CreateTableOfContents(IEnumerable<AssetPackSpecification> assetPackSpecifications);
}



public class TableOfContentsGenerator(
	IBasicAssetReader basicAssetReader
) : ITableOfContentsGenerator
{
	public TableOfContents CreateTableOfContents(IEnumerable<AssetPackSpecification> assetPackSpecifications) =>
		new()
		{
			ResourceIdentifiers =
				assetPackSpecifications
					.SelectMany(CreateEntries)
					.ToDictionary(
						x => x.AssetId.Id,
						x => new ContentEntry
						{
							PackFileName = Conventions.CreateAssetPackName(x.PackageName),
							FilePath = x.FilePath.Value
						}
					)
		};


	private IEnumerable<Entry> CreateEntries(AssetPackSpecification assetPackSpecification) =>
		assetPackSpecification
			.FileSpecifications
			.Where(x => x.RelativePath.Value.EndsWith(AssetConventions.AssetFileEnding))
			.Select(
				x => new Entry(
					assetPackSpecification.PackageName,
					basicAssetReader.ReadFile(x.AbsolutePath.Value).Id,
					x.RelativePath
				)
			);


	private record Entry(PackageName PackageName, AssetId AssetId, NormalizedPath FilePath);
}
