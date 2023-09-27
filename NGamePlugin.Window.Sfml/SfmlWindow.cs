using NGame.OsWindows;
using NGame.Renderers;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private readonly IRenderTexture _renderTexture;


	private Texture? _texture2;
	private Sprite? _sprite;
	private RenderWindow? _window;


	public SfmlWindow(IRenderTexture renderTexture)
	{
		_renderTexture = renderTexture;
	}


	public event EventHandler? Closed;


	private CancellationTokenSource? CancellationTokenSource { get; set; }


	public void Initialize(CancellationTokenSource cancellationTokenSource)
	{
		CancellationTokenSource = cancellationTokenSource;

		_texture2 = new Texture(250, 250);
		_sprite = new Sprite(_texture2);

		VideoMode mode = new VideoMode(250, 250);
		_window = new RenderWindow(mode, "SFML.NET");

		_window.Closed += OnClosed;
	}


	public void Draw()
	{
		_window!.DispatchEvents();
		_window.Clear();

		var pixels = _renderTexture.GetPixels();
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
