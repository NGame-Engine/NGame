namespace NGame.Inputs;

public class JoystickConnectedEventArgs : EventArgs
{
	public JoystickConnectedEventArgs(int joystickId)
	{
		JoystickId = joystickId;
	}


	public int JoystickId { get; }
}
