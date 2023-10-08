namespace NGame.Inputs;

public interface IDesktopRawInputListener
{
	event EventHandler<TextEnteredEventArgs> TextEntered;
	event EventHandler<KeyPressedEventArgs> KeyPressed;
	event EventHandler<KeyReleasedEventArgs> KeyReleased;
	event EventHandler<MouseWheelScrolledEventArgs> MouseWheelScrolled;
	event EventHandler<MouseButtonPressedEventArgs> MouseButtonPressed;
	event EventHandler<MouseButtonReleasedEventArgs> MouseButtonReleased;
	event EventHandler<MouseMovedEventArgs> MouseMoved;
	event EventHandler MouseEntered;
	event EventHandler MouseLeft;
	event EventHandler<JoystickButtonPressedEventArgs> JoystickButtonPressed;
	event EventHandler<JoystickButtonReleasedEventArgs> JoystickButtonReleased;
	event EventHandler<JoystickAxisMovedEventArgs> JoystickAxisMoved;
	event EventHandler<JoystickConnectedEventArgs> JoystickConnected;
	event EventHandler<JoystickDisconnectedEventArgs> JoystickDisconnected;
}
