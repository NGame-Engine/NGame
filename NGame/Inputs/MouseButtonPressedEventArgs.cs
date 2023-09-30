﻿using System.Drawing;

namespace NGame.Inputs;

public sealed class MouseButtonPressedEventArgs : EventArgs
{
	public MouseButtonPressedEventArgs(MouseButton mouseButton, Point position)
	{
		MouseButton = mouseButton;
		Position = position;
	}


	public MouseButton MouseButton { get; }
	public Point Position { get; }
}
