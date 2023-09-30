using System.Numerics;

namespace NGame.Components.Texts;

public sealed class Text
{
	public Text(Font font, string content)
	{
		Font = font;
		Content = content;
	}


	public Font Font { get; set; }
	public string Content { get; set; }
	public int CharacterSize { get; set; }
	public Vector2 TransformOrigin { get; set; }
}
