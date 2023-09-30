namespace NGame.Inputs;

public class JoystickDisconnectedEventArgs : EventArgs
{
	public JoystickDisconnectedEventArgs(int joystickId)
	{
		JoystickId = joystickId;
	}


	public int JoystickId { get; }
}
