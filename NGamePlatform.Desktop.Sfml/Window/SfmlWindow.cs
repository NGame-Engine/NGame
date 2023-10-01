using System.Drawing;
using NGame.OsWindows;
using SFML.Window;

namespace NGamePlatform.Desktop.Sfml.Window;



public class SfmlWindow : IOsWindow
{
	public event EventHandler<ResizedEventArgs>? Resized;
	public event EventHandler? FocusLost;
	public event EventHandler? FocusGained;


	public void OnResized(object? sender, SizeEventArgs e)
	{
		var width = (int)e.Width;
		var height = (int)e.Height;
		var newSize = new Size(width, height);
		var eventArgs = new ResizedEventArgs(newSize);
		Resized?.Invoke(this, eventArgs);
	}


	public void OnLostFocus(object? sender, EventArgs e)
	{
		FocusLost?.Invoke(this, EventArgs.Empty);
	}


	public void OnGainedFocus(object? sender, EventArgs e)
	{
		FocusGained?.Invoke(this, EventArgs.Empty);
	}
}
