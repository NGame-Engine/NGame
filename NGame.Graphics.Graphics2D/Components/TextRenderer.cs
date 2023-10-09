using System.Drawing;
using System.Numerics;
using NGame.Assets;

namespace NGame.Components;



public sealed class Text
{
	public Text(Font font, string content)
	{
		Font = font;
		Content = content;
	}


	public Font Font;
	public string Content;
	public int CharacterSize;
	public Color Color;
	public Vector2 TransformOrigin;
}



public sealed class TextRenderer : Renderer2D
{
	public Text? Text { get; set; }
}
