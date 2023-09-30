using System.Drawing;

namespace NGame.OsWindows;

public class MouseMovedEventArgs : EventArgs
{
	public MouseMovedEventArgs(Point position)
	{
		Position = position;
	}


	public Point Position { get; }
}
