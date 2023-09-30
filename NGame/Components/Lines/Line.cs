using System.Numerics;

namespace NGame.Components.Lines;

public sealed class Line
{
	public List<Vector2> Vertices = new();
	public float Width { get; set; } = 1f;
}
