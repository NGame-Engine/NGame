﻿namespace NGame.Inputs;

public class JoystickButtonReleasedEventArgs : EventArgs
{
	public JoystickButtonReleasedEventArgs(int joystickId, int button)
	{
		JoystickId = joystickId;
		Button = button;
	}


	public int JoystickId { get; }
	public int Button { get; }
}