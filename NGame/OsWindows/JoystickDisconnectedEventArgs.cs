using System.Drawing;

namespace NGame.OsWindows;

public class JoystickDisconnectedEventArgs : EventArgs
{
	public JoystickDisconnectedEventArgs(int joystickId)
	{
		JoystickId = joystickId;
	}


	public int JoystickId { get; }
}
