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


	private CancellationTokenSource? CancellationTokenSource { get; set; }


	public void Initialize(CancellationTokenSource cancellationTokenSource)
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
	}


	public void Draw(byte[] pixels)
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
}
