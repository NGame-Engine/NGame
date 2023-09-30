using System.Drawing;

namespace NGame.Sprites;

public sealed class Sprite
{
	public Texture Texture { get; set; }
	public Rectangle SourceRectangle { get; set; }
	public Rectangle TargetRectangle { get; set; }
}
