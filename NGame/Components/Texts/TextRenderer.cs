using NGame.Ecs;

namespace NGame.Components.Texts;

public sealed class TextRenderer : IComponent
{
	public Text? Text { get; set; }
}
