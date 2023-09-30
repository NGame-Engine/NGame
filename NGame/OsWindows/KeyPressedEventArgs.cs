using System.Drawing;

namespace NGame.OsWindows;

public class KeyPressedEventArgs : EventArgs
{
	public KeyPressedEventArgs(KeyCode keyCode)
	{
		KeyCode = keyCode;
	}


	public KeyCode KeyCode { get; }
}
