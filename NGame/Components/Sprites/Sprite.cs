using System.Drawing;

namespace NGame.Components.Sprites;

public sealed class Sprite
{
	public Texture Texture { get; set; }
	public Rectangle SourceRectangle { get; set; }
}
