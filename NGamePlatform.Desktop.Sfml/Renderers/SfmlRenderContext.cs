using NGame.UpdateSchedulers;
using SFML.Graphics;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class SfmlRenderContext : IRenderContext
{
	private readonly RenderWindow _renderWindow;


	public SfmlRenderContext(RenderWindow renderWindow)
	{
		_renderWindow = renderWindow;
	}


	public bool BeginDraw()
	{
		_renderWindow.DispatchEvents();
		_renderWindow.Clear();

		return true;
	}


	public void EndDraw(bool shouldPresent)
	{
		if (!shouldPresent) return;

		_renderWindow.Display();
	}
}
