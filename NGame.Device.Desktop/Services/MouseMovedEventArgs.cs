using System.Drawing;

namespace NGame.Services;

public sealed class MouseMovedEventArgs : EventArgs
{
	public MouseMovedEventArgs(Point position)
	{
		Position = position;
	}


	public Point Position { get; }
}
