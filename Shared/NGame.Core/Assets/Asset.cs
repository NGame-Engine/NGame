using Semver;

namespace NGame.Assets;



public abstract class Asset
{
	public Guid Id { get; init; }
	public SemVersion SerializerVersion { get; init; } = new(0);
}
