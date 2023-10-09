namespace NGame.Services;

public sealed class JoystickAxisMovedEventArgs : EventArgs
{
	public JoystickAxisMovedEventArgs(int joystickId, JoystickAxis joystickAxis, float value)
	{
		JoystickId = joystickId;
		JoystickAxis = joystickAxis;
		Value = value;
	}


	public int JoystickId { get; }
	public JoystickAxis JoystickAxis { get; }
	public float Value { get; }
}
