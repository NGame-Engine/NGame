using NGame.Assets;
using NGame.Ecs;

namespace NGame.SceneAssets;

[Asset(Discriminator = "NGame.SceneAsset", Name = "NGame Scene")]
public class SceneAsset : Asset
{
	public List<EntityEntry> Entities { get; init; } = [];
}



public class EntityEntry
{
	public Guid Id { get; init; }
	public string Name { get; set; } = "";
	public List<EntityComponent> Components { get; init; } = [];
	public List<EntityEntry> Children { get; init; } = [];
}
