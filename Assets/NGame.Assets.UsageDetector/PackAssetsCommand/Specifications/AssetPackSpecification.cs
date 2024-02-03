using NGame.Cli.PackAssetsCommand.AssetFileReaders;
using NGame.Cli.PackAssetsCommand.Paths;

namespace NGame.Cli.PackAssetsCommand.Specifications;



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

