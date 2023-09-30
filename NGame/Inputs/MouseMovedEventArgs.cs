using System.Drawing;

namespace NGame.Inputs;

public class MouseMovedEventArgs : EventArgs
{
	public MouseMovedEventArgs(Point position)
	{
		Position = position;
	}


	public Point Position { get; }
}
