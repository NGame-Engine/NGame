using System.Drawing;
using System.Numerics;

namespace NGame.Components.Lines;



public sealed class Line
{
	public List<Vector2> Vertices = new();
	public Color Color = Color.White;
	public float Width = 1f;
}
