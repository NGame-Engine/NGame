using NGame.Cli.Abstractions.Paths;
using NGame.Cli.PackAssets.AssetFileReaders;

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

[Obsolete]
public record AssetFileSpecification(AbsoluteNormalizedPath AbsolutePath, NormalizedPath RelativePath);

