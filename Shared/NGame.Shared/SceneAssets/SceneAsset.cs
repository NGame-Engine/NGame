using NGame.Assets;
using NGame.Ecs;

namespace NGame.SceneAssets;

[Asset(Discriminator = "NGame.SceneAsset")]
public class SceneAsset : Asset
{
	[Obsolete] public HashSet<AssetId> Assets { get; init; } = new();
	public List<EntityEntry> Entities { get; init; } = new();
}



public class EntityEntry
{
	public Guid Id { get; init; }
	public string Name { get; set; } = "";
	public List<EntityComponent> Components { get; init; } = new();
	public List<EntityEntry> Children { get; init; } = new();
}
