using NGame.OsWindows;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private RenderWindow? _window;


	public event EventHandler? Closed;


	public object? RenderTexture { get; }
	private CancellationTokenSource CancellationTokenSource { get; set; }


	public void Initialize(CancellationTokenSource cancellationTokenSource)
	{
		CancellationTokenSource = cancellationTokenSource;

		VideoMode mode = new VideoMode(250, 250);
		_window = new RenderWindow(mode, "SFML.NET");

		_window.Closed += OnClosed;
	}


	public void Draw()
	{
		_window!.DispatchEvents();
		_window.Clear();

		_window.Display();
	}


	private void OnClosed(object? sender, EventArgs e)
	{
		_window?.Close();
		CancellationTokenSource.Cancel();
		Closed?.Invoke(this, e);
	}
}
