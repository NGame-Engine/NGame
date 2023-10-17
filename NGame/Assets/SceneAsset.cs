using NGame.Components;

namespace NGame.Assets;



[Asset(Discriminator = "NGame.SceneAsset")]
public class SceneAsset : Asset
{
	public List<EntityEntry> Entities { get; init; }
}



public class EntityEntry
{
	public List<Component> Components { get; init; } = new();
}
