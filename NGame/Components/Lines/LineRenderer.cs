using NGame.Ecs;

namespace NGame.Components.Lines;



public sealed class LineRenderer : IComponent
{
	public Line? Line { get; set; }
}
