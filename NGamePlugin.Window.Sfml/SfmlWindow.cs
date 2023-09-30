using System.Drawing;
using Microsoft.Extensions.Hosting;
using NGame.OsWindows;
using NGame.Renderers;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private readonly GraphicsConfiguration _graphicsConfiguration;
	private readonly IHostEnvironment _hostEnvironment;

	private Texture? _texture2;
	private Sprite? _sprite;
	private RenderWindow? _window;


	public SfmlWindow(GraphicsConfiguration graphicsConfiguration, IHostEnvironment hostEnvironment)
	{
		_graphicsConfiguration = graphicsConfiguration;
		_hostEnvironment = hostEnvironment;
	}


	public event EventHandler? Closed;
	public event EventHandler<ResizedEventArgs>? Resized;
	public event EventHandler? FocusLost;
	public event EventHandler? FocusGained;
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


	private CancellationTokenSource? CancellationTokenSource { get; set; }


	void IOsWindow.Initialize(CancellationTokenSource cancellationTokenSource)
	{
		CancellationTokenSource = cancellationTokenSource;

		var width = _graphicsConfiguration.Width;
		var height = _graphicsConfiguration.Height;

		_texture2 = new Texture(width, height);
		_sprite = new Sprite(_texture2);

		var mode = new VideoMode(width, height);
		var title = _hostEnvironment.ApplicationName;
		_window = new RenderWindow(mode, title);

		_window.Closed += OnClosed;
		_window.Resized += OnResized;
		_window.LostFocus += OnLostFocus;
		_window.GainedFocus += OnGainedFocus;
		_window.TextEntered += OnTextEntered;
		_window.KeyPressed += OnKeyPressed;
		_window.KeyReleased += OnKeyReleased;
		_window.MouseWheelScrolled += OnMouseWheelScrolled;
		_window.MouseButtonPressed += OnMouseButtonPressed;
		_window.MouseButtonReleased += OnMouseButtonReleased;
		_window.MouseMoved += OnMouseMoved;
		_window.MouseEntered += OnMouseEntered;
		_window.MouseLeft += OnMouseLeft;
		_window.JoystickButtonPressed += OnJoystickButtonPressed;
		_window.JoystickButtonReleased += OnJoystickButtonReleased;
		_window.JoystickMoved += OnJoystickMoved;
		_window.JoystickConnected += OnJoystickConnected;
		_window.JoystickDisconnected += OnJoystickDisconnected;
	}


	void IOsWindow.Draw(byte[] pixels)
	{
		_window!.DispatchEvents();
		_window.Clear();

		_texture2!.Update(pixels);
		_window.Draw(_sprite);

		_window.Display();
	}


	private void OnClosed(object? sender, EventArgs e)
	{
		_window?.Close();
		CancellationTokenSource!.Cancel();
		Closed?.Invoke(this, e);
	}


	private void OnResized(object? sender, SizeEventArgs e)
	{
		var width = (int)e.Width;
		var height = (int)e.Height;
		var newSize = new Size(width, height);
		var eventArgs = new ResizedEventArgs(newSize);
		Resized?.Invoke(this, eventArgs);
	}


	private void OnLostFocus(object? sender, EventArgs e)
	{
		FocusLost?.Invoke(this, EventArgs.Empty);
	}


	private void OnGainedFocus(object? sender, EventArgs e)
	{
		FocusGained?.Invoke(this, EventArgs.Empty);
	}


	private void OnTextEntered(object? sender, TextEventArgs e)
	{
		var text = e.Unicode;
		var eventArgs = new TextEnteredEventArgs(text);
		TextEntered?.Invoke(this, eventArgs);
	}


	private void OnKeyPressed(object? sender, KeyEventArgs e)
	{
		var keyCode = KeyMapper.Map(e.Code);
		var eventArgs = new KeyPressedEventArgs(keyCode);
		KeyPressed?.Invoke(this, eventArgs);
	}


	private void OnKeyReleased(object? sender, KeyEventArgs e)
	{
		var keyCode = KeyMapper.Map(e.Code);
		var eventArgs = new KeyReleasedEventArgs(keyCode);
		KeyReleased?.Invoke(this, eventArgs);
	}


	private void OnMouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
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


	private void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e)
	{
		var mouseButton = MouseButtonMapper.Map(e.Button);
		var position = new Point(e.X, e.Y);
		var eventArgs = new MouseButtonPressedEventArgs(mouseButton, position);
		MouseButtonPressed?.Invoke(this, eventArgs);
	}


	private void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e)
	{
		var mouseButton = MouseButtonMapper.Map(e.Button);
		var position = new Point(e.X, e.Y);
		var eventArgs = new MouseButtonReleasedEventArgs(mouseButton, position);
		MouseButtonReleased?.Invoke(this, eventArgs);
	}


	private void OnMouseMoved(object? sender, MouseMoveEventArgs e)
	{
		var position = new Point(e.X, e.Y);
		MouseMoved?.Invoke(this, new MouseMovedEventArgs(position));
	}


	private void OnMouseEntered(object? sender, EventArgs e)
	{
		MouseEntered?.Invoke(this, EventArgs.Empty);
	}


	private void OnMouseLeft(object? sender, EventArgs e)
	{
		MouseLeft?.Invoke(this, EventArgs.Empty);
	}


	private void OnJoystickButtonPressed(object? sender, JoystickButtonEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var button = (int)e.Button;
		var eventArgs = new JoystickButtonPressedEventArgs(joystickId, button);
		JoystickButtonPressed?.Invoke(this, eventArgs);
	}


	private void OnJoystickButtonReleased(object? sender, JoystickButtonEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var button = (int)e.Button;
		var eventArgs = new JoystickButtonReleasedEventArgs(joystickId, button);
		JoystickButtonReleased?.Invoke(this, eventArgs);
	}


	private void OnJoystickMoved(object? sender, JoystickMoveEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var axis = JoystickAxisMapper.Map(e.Axis);
		var value = e.Position;
		var eventArgs = new JoystickAxisMovedEventArgs(joystickId, axis, value);
		JoystickAxisMoved?.Invoke(this, eventArgs);
	}


	private void OnJoystickConnected(object? sender, JoystickConnectEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var eventArgs = new JoystickConnectedEventArgs(joystickId);
		JoystickConnected?.Invoke(this, eventArgs);
	}


	private void OnJoystickDisconnected(object? sender, JoystickConnectEventArgs e)
	{
		var joystickId = (int)e.JoystickId;
		var eventArgs = new JoystickDisconnectedEventArgs(joystickId);
		JoystickDisconnected?.Invoke(this, eventArgs);
	}
}
