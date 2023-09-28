using NGame.OsWindows;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private Texture? _texture2;
	private Sprite? _sprite;
	private RenderWindow? _window;


	public event EventHandler? Closed;
	public IntPtr PixelsPointer => _texture2.CPointer;


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
