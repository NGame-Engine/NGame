using System.Drawing;

namespace NGame.OsWindows;

public class JoystickButtonPressedEventArgs : EventArgs
{
	public JoystickButtonPressedEventArgs(int joystickId, int button)
	{
		JoystickId = joystickId;
		Button = button;
	}


	public int JoystickId { get; }
	public int Button { get; }
}
