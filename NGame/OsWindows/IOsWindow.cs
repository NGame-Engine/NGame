using System.Numerics;

namespace NGame.OsWindows;

public interface IOsWindow
{
	event EventHandler Closed;
	event EventHandler<ResizedEventArgs> Resized;
	event EventHandler FocusLost;
	event EventHandler FocusGained;
	event EventHandler<TextEnteredEventArgs> TextEntered ;
	event EventHandler<KeyPressedEventArgs> KeyPressed ;
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




	void Initialize(CancellationTokenSource cancellationTokenSource);
	void Draw(byte[] pixels);
}
