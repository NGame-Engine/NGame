using System.Numerics;

namespace NGame.Renderers;

public class Line
{
	public List<Vector2> Vertices = new();
	public float Width { get; set; } = 1f;
}
