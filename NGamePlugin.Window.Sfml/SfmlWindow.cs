using NGame.OsWindows;
using NGame.Renderers;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private readonly GraphicsConfiguration _graphicsConfiguration;

	private Texture? _texture2;
	private Sprite? _sprite;
	private RenderWindow? _window;


	public SfmlWindow(GraphicsConfiguration graphicsConfiguration)
	{
		_graphicsConfiguration = graphicsConfiguration;
	}


	public event EventHandler? Closed;
	public IntPtr PixelsPointer => _texture2.CPointer;


	private CancellationTokenSource? CancellationTokenSource { get; set; }


	public void Initialize(CancellationTokenSource cancellationTokenSource)
	{
		CancellationTokenSource = cancellationTokenSource;

		var width = _graphicsConfiguration.Width;
		var height = _graphicsConfiguration.Height;

		_texture2 = new Texture(width, height);
		_sprite = new Sprite(_texture2);

		VideoMode mode = new VideoMode(width, height);
		_window = new RenderWindow(mode, "SFML.NET");

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
