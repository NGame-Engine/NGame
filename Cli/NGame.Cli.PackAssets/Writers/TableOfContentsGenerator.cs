using NGame.Assets;
using NGame.Cli.Abstractions.Paths;
using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.Specifications;
using NGame.Tooling.Assets;

namespace NGame.Cli.PackAssets.Writers;



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
