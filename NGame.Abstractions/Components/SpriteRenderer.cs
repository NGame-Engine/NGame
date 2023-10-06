using NGame.Assets;

namespace NGame.Components;

[Component(StableDiscriminator = "NGame.Sprites.SpriteRenderer")]
public sealed class SpriteRenderer : Renderer2D
{
	public Sprite? Sprite { get; set; }
}
