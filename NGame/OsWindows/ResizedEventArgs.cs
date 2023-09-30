using System.Drawing;

namespace NGame.OsWindows;

public sealed class ResizedEventArgs : EventArgs
{
	public ResizedEventArgs(Size newSize)
	{
		NewSize = newSize;
	}


	public Size NewSize { get; }
}
