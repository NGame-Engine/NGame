using System.Drawing;
using NGame.OsWindows;
using NGame.Renderers;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlatform.Desktop.Sfml.Window;

public class SfmlWindow : IOsWindow
{
	private readonly RenderWindow _window;
	private readonly Texture _texture;
	private readonly Sprite _sprite;


	public SfmlWindow(
		RenderWindow renderWindow,
		GraphicsConfiguration graphicsConfiguration
	)
	{
		_window = renderWindow;

		var width = (uint)graphicsConfiguration.Width;
		var height = (uint)graphicsConfiguration.Height;
		_texture = new Texture(width, height);

		_sprite = new Sprite(_texture);
	}


	public event EventHandler<ResizedEventArgs>? Resized;
	public event EventHandler? FocusLost;
	public event EventHandler? FocusGained;


	void IOsWindow.Draw(byte[] pixels)
	{
		_window.DispatchEvents();
		_window.Clear();

		_texture.Update(pixels);
		_window.Draw(_sprite);

		_window.Display();
	}


	internal void OnResized(object? sender, SizeEventArgs e)
	{
		var width = (int)e.Width;
		var height = (int)e.Height;
		var newSize = new Size(width, height);
		var eventArgs = new ResizedEventArgs(newSize);
		Resized?.Invoke(this, eventArgs);
	}


	internal void OnLostFocus(object? sender, EventArgs e)
	{
		FocusLost?.Invoke(this, EventArgs.Empty);
	}


	internal void OnGainedFocus(object? sender, EventArgs e)
	{
		FocusGained?.Invoke(this, EventArgs.Empty);
	}
}
