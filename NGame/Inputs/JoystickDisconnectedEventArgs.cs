namespace NGame.Inputs;

public sealed class JoystickDisconnectedEventArgs : EventArgs
{
	public JoystickDisconnectedEventArgs(int joystickId)
	{
		JoystickId = joystickId;
	}


	public int JoystickId { get; }
}
