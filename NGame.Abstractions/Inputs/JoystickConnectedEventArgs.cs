namespace NGame.Inputs;

public sealed class JoystickConnectedEventArgs : EventArgs
{
	public JoystickConnectedEventArgs(int joystickId)
	{
		JoystickId = joystickId;
	}


	public int JoystickId { get; }
}
