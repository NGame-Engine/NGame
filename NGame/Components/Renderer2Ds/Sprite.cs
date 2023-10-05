using System.Drawing;

namespace NGame.Components.Renderer2Ds;

public sealed class Sprite
{
	public Texture Texture { get; set; }
	public Rectangle SourceRectangle { get; set; }
}
