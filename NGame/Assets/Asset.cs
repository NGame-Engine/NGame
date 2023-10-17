using Semver;

namespace NGame.Assets;



public abstract class Asset
{
	public Guid Id { get; init; }
	
	
	public SemVersion SerializerVersion { get; init; } = new(0);
}



public abstract class FileAsset : Asset
{
	public string FilePath { get; init; }
}
