using System.Drawing;
using System.Numerics;

namespace NGame.Components;



public sealed class Line
{
	public List<Vector2> Vertices = new();
	public Color Color = Color.White;
	public float Width = 1f;
}



public sealed class LineRenderer : Renderer2D
{
	public Line? Line { get; set; }
}
