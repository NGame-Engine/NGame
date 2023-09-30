using System.Drawing;
using System.Numerics;

namespace NGame.Components.Texts;



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
