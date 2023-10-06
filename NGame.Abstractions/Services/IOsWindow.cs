using System.Drawing;

namespace NGame.Services;



public sealed class ResizedEventArgs : EventArgs
{
	public ResizedEventArgs(Size newSize)
	{
		NewSize = newSize;
	}


	public Size NewSize { get; }
}



public interface IOsWindow
{
	event EventHandler<ResizedEventArgs> Resized;
	event EventHandler FocusLost;
	event EventHandler FocusGained;
}
