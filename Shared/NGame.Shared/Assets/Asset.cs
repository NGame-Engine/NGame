using Semver;

namespace NGame.Assets;



public abstract class Asset
{
	public AssetId Id { get; init; }
	public SemVersion SerializerVersion { get; init; } = new(0);
}
