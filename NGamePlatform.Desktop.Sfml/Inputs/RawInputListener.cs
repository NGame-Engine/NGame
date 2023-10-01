using System.Drawing;
using NGame.Inputs;
using SFML.Window;

namespace NGamePlatform.Desktop.Sfml.Inputs;

internal class RawInputListener : IRawInputListener
{
	public event EventHandler<TextEnteredEventArgs>? TextEntered;
	public event EventHandler<KeyPressedEventArgs>? KeyPressed;
	public event EventHandler<KeyReleasedEventArgs>? KeyReleased;
	public event EventHandler<MouseWheelScrolledEventArgs>? MouseWheelScrolled;
	public event EventHandler<MouseButtonPressedEventArgs>? MouseButtonPressed;
	public event EventHandler<MouseButtonReleasedEventArgs>? MouseButtonReleased;
	public event EventHandler<MouseMovedEventArgs>? MouseMoved;
	public event EventHandler? MouseEntered;
	public event EventHandler? MouseLeft;
	public event EventHandler<JoystickButtonPressedEventArgs>? JoystickButtonPressed;
	public event EventHandler<JoystickButtonReleasedEventArgs>? JoystickButtonReleased;
	public event EventHandler<JoystickAxisMovedEventArgs>? JoystickAxisMoved;
	public event EventHandler<JoystickConnectedEventArgs>? JoystickConnected;
	public event EventHandler<JoystickDisconnectedEventArgs>? JoystickDisconnected;


	internal void OnTextEntered(object? sender, TextEventArgs e)
	{
		var text = e.Unicode;
		var eventArgs = new TextEnteredEventArgs(text);
		TextEntered?.Invoke(this, eventArgs);
	}


	internal void OnKeyPressed(object? sender, KeyEventArgs e)
	{
		var keyCode = KeyMapper.Map(e.Code);
		var eventArgs = new KeyPressedEventArgs(keyCode);
		KeyPressed?.Invoke(this, eventArgs);
	}


	internal void OnKeyReleased(object? sender, KeyEventArgs e)
	{
		var keyCode = KeyMapper.Map(e.Code);
		var eventArgs = new KeyReleasedEventArgs(keyCode);
		KeyReleased?.Invoke(this, eventArgs);
	}


	internal void OnMouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
	{
		var direction =
			e.Wheel == Mouse.Wheel.HorizontalWheel
				? MouseWheelDirection.Horizontal
				: MouseWheelDirection.Vertical;

		var delta = e.Delta;
		var position = new Point(e.X, e.Y);
		var eventArgs = new MouseWheelScrolledEventArgs(direction, delta, position);
		MouseWheelScrolled?.Invoke(this, eventArgs);
	}


	internal void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e)
	{
		var mouseButton = MouseButtonMapper.Map(e.Button);
		var position = new Point(e.X, e.Y);
		var eventArgs = new MouseButtonPressedEventArgs(mouseButton, position);
		MouseButtonPressed?.Invoke(this, eventArgs);
	}


	internal void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e)
	{
		var mouseButton = MouseButtonMapper.Map(e.Button);
		var position = new Point(e.X, e.Y);
		var eventArgs = new MouseButtonReleasedEventArgs(mouseButton, position);
		MouseButtonReleased?.Invoke(this, eventArgs);
	}


	internal void OnMouseMoved(object? sender, MouseMoveEventArgs e)
	{
		var position = new Point(e.X, e.Y);
		MouseMoved?.Invoke(this, new MouseMovedEventArgs(position));
	}


	internal void OnMouseEntered(object? sender, EventArgs e)
	{
		MouseEntered?.Invoke(this, EventArgs.Empty);
	}


	internal void OnMouseLeft(object? sender, EventArgs e)
	{
		MouseLeft?.Invoke(this, EventArgs.Empty);
	}


	internal void OnJoystickButtonPressed(object? sender, JoystickButtonEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var button = (int)e.Button;
		var eventArgs = new JoystickButtonPressedEventArgs(joystickId, button);
		JoystickButtonPressed?.Invoke(this, eventArgs);
	}


	internal void OnJoystickButtonReleased(object? sender, JoystickButtonEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var button = (int)e.Button;
		var eventArgs = new JoystickButtonReleasedEventArgs(joystickId, button);
		JoystickButtonReleased?.Invoke(this, eventArgs);
	}


	internal void OnJoystickMoved(object? sender, JoystickMoveEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var axis = JoystickAxisMapper.Map(e.Axis);
		var value = e.Position;
		var eventArgs = new JoystickAxisMovedEventArgs(joystickId, axis, value);
		JoystickAxisMoved?.Invoke(this, eventArgs);
	}


	internal void OnJoystickConnected(object? sender, JoystickConnectEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var eventArgs = new JoystickConnectedEventArgs(joystickId);
		JoystickConnected?.Invoke(this, eventArgs);
	}


	internal void OnJoystickDisconnected(object? sender, JoystickConnectEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var eventArgs = new JoystickDisconnectedEventArgs(joystickId);
		JoystickDisconnected?.Invoke(this, eventArgs);
	}
}
