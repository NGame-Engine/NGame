using NGame.Cli.PackAssets.AssetFileReaders;
using NGame.Cli.PackAssets.Paths;

namespace NGame.Cli.PackAssets.Specifications;



public class AssetPackSpecification
{
	public AssetPackSpecification(
		PackageName packageName,
		ICollection<AssetFileSpecification> assetFileSpecifications
	)
	{
		PackageName = packageName;
		FileSpecifications = assetFileSpecifications;
	}


	public PackageName PackageName { get; }
	public ICollection<AssetFileSpecification> FileSpecifications { get; }
}


public record AssetFileSpecification(AbsoluteNormalizedPath AbsolutePath, NormalizedPath RelativePath);

