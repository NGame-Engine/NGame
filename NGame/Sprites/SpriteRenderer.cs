using NGame.Assets;
using NGame.Ecs;

namespace NGame.Sprites;

[Component(StableDiscriminator = "NGame.Sprites.SpriteRenderer")]
public class SpriteRenderer : IComponent
{
	public Sprite? Sprite { get; set; }
}
