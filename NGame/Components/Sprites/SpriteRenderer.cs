using NGame.Assets;
using NGame.Ecs;

namespace NGame.Components.Sprites;

[Component(StableDiscriminator = "NGame.Sprites.SpriteRenderer")]
public sealed class SpriteRenderer : Component
{
	public Sprite? Sprite { get; set; }
}
