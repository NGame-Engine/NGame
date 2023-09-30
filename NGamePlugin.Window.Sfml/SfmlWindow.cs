using System.Drawing;
using NGame.OsWindows;
using NGame.Renderers;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private readonly Texture _texture;
	private readonly Sprite _sprite;
	private readonly RenderWindow _window;


	public SfmlWindow(RenderWindow renderWindow, GraphicsConfiguration graphicsConfiguration)
	{
		_window = renderWindow;

		var width = graphicsConfiguration.Width;
		var height = graphicsConfiguration.Height;
		_texture = new Texture(width, height);

		_sprite = new Sprite(_texture);
	}


	public event EventHandler? Closed;
	public event EventHandler<ResizedEventArgs>? Resized;
	public event EventHandler? FocusLost;
	public event EventHandler? FocusGained;


	private CancellationTokenSource? CancellationTokenSource { get; set; }


	void IOsWindow.Initialize(CancellationTokenSource cancellationTokenSource)
	{
		CancellationTokenSource = cancellationTokenSource;
	}


	void IOsWindow.Draw(byte[] pixels)
	{
		_window.DispatchEvents();
		_window.Clear();

		_texture.Update(pixels);
		_window.Draw(_sprite);

		_window.Display();
	}


	internal void OnClosed(object? sender, EventArgs e)
	{
		_window.Close();
		CancellationTokenSource!.Cancel();
		Closed?.Invoke(this, e);
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
