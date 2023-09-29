using System.Drawing;

namespace NGame.Sprites;

public class RendererSprite
{
	public RendererSprite(Texture texture)
	{
		Texture = texture;
	}


	public Texture Texture { get; set; }
	public Rectangle SourceRectangle { get; set; }
	public Rectangle TargetRectangle { get; set; }
}
