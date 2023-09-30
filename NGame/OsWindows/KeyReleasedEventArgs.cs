using System.Drawing;

namespace NGame.OsWindows;

public class KeyReleasedEventArgs : EventArgs
{
	public KeyReleasedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
