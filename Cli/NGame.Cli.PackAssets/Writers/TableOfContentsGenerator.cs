using NGame.Assets;
using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.Paths;
using NGame.Cli.PackAssets.Specifications;

namespace NGame.Cli.PackAssets.Writers;



public interface ITableOfContentsGenerator
{
	TableOfContents CreateTableOfContents(IEnumerable<AssetPackSpecification> assetPackSpecifications);
}



public class TableOfContentsGenerator : ITableOfContentsGenerator
{
	private readonly IBasicAssetReader _basicAssetReader;


	public TableOfContentsGenerator(IBasicAssetReader basicAssetReader)
	{
		_basicAssetReader = basicAssetReader;
	}


	public TableOfContents CreateTableOfContents(IEnumerable<AssetPackSpecification> assetPackSpecifications) =>
		new()
		{
			ResourceIdentifiers =
				assetPackSpecifications
					.SelectMany(CreateEntries)
					.ToDictionary(
						x => x.AssetId,
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
					_basicAssetReader.ReadFile(x.AbsolutePath.Value).Id,
					x.RelativePath
				)
			);


	private record Entry(PackageName PackageName, AssetId AssetId, NormalizedPath FilePath);
}
