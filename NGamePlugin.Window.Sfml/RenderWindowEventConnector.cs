using NGamePlugin.Window.Sfml.Inputs;
using SFML.Graphics;

namespace NGamePlugin.Window.Sfml;

internal class RenderWindowEventConnector
{
	private readonly RenderWindow _renderWindow;
	private readonly SfmlWindow _sfmlWindow;
	private readonly RawInputListener _rawInputListener;


	public RenderWindowEventConnector(
		RenderWindow renderWindow,
		SfmlWindow sfmlWindow,
		RawInputListener rawInputListener
	)
	{
		this._renderWindow = renderWindow;
		_sfmlWindow = sfmlWindow;
		_rawInputListener = rawInputListener;
	}


	public void ConnectEvents()
	{
		_renderWindow.Closed += _sfmlWindow.OnClosed;
		_renderWindow.Resized += _sfmlWindow.OnResized;
		_renderWindow.LostFocus += _sfmlWindow.OnLostFocus;
		_renderWindow.GainedFocus += _sfmlWindow.OnGainedFocus;

		_renderWindow.TextEntered += _rawInputListener.OnTextEntered;
		_renderWindow.KeyPressed += _rawInputListener.OnKeyPressed;
		_renderWindow.KeyReleased += _rawInputListener.OnKeyReleased;
		_renderWindow.MouseWheelScrolled += _rawInputListener.OnMouseWheelScrolled;
		_renderWindow.MouseButtonPressed += _rawInputListener.OnMouseButtonPressed;
		_renderWindow.MouseButtonReleased += _rawInputListener.OnMouseButtonReleased;
		_renderWindow.MouseMoved += _rawInputListener.OnMouseMoved;
		_renderWindow.MouseEntered += _rawInputListener.OnMouseEntered;
		_renderWindow.MouseLeft += _rawInputListener.OnMouseLeft;
		_renderWindow.JoystickButtonPressed += _rawInputListener.OnJoystickButtonPressed;
		_renderWindow.JoystickButtonReleased += _rawInputListener.OnJoystickButtonReleased;
		_renderWindow.JoystickMoved += _rawInputListener.OnJoystickMoved;
		_renderWindow.JoystickConnected += _rawInputListener.OnJoystickConnected;
		_renderWindow.JoystickDisconnected += _rawInputListener.OnJoystickDisconnected;
	}
}
