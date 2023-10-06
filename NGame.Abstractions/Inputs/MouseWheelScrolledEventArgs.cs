using System.Drawing;

namespace NGame.Inputs;

public sealed class MouseWheelScrolledEventArgs : EventArgs
{
	public MouseWheelScrolledEventArgs(MouseWheelDirection direction, float delta, Point position)
	{
		Direction = direction;
		Delta = delta;
		Position = position;
	}


	public MouseWheelDirection Direction { get; }
	public float Delta { get; }
	public Point Position { get; }
}
