using System.Drawing;

namespace NGame.OsWindows;

public class ResizedEventArgs : EventArgs
{
	public ResizedEventArgs(Size newSize)
	{
		NewSize = newSize;
	}


	public Size NewSize { get; }
}
