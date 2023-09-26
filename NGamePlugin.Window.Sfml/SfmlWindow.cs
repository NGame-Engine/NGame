using NGame.OsWindows;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

public class SfmlWindow : IOsWindow
{
	private RenderWindow? _window;


	public event EventHandler? Closed;
	
	
	public object? RenderTexture { get; }


	public void Initialize()
	{
		VideoMode mode = new VideoMode(250, 250);
		_window = new RenderWindow(mode, "SFML.NET");
		
		_window.Closed += OnClosed;
	}


	private void OnClosed(object? sender, EventArgs e)
	{
		_window?.Close();
		Closed?.Invoke(this, e);
	}
}
