namespace NGame.Services;

public sealed class JoystickButtonPressedEventArgs : EventArgs
{
	public JoystickButtonPressedEventArgs(int joystickId, int button)
	{
		JoystickId = joystickId;
		Button = button;
	}


	public int JoystickId { get; }
	public int Button { get; }
}
