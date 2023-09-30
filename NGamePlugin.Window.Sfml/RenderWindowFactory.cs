using Microsoft.Extensions.Hosting;
using NGame.Renderers;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlugin.Window.Sfml;

internal class RenderWindowFactory
{
	private readonly GraphicsConfiguration _graphicsConfiguration;
	private readonly IHostEnvironment _hostEnvironment;


	public RenderWindowFactory(GraphicsConfiguration graphicsConfiguration, IHostEnvironment hostEnvironment)
	{
		_graphicsConfiguration = graphicsConfiguration;
		_hostEnvironment = hostEnvironment;
	}


	public RenderWindow Create()
	{
		var width = _graphicsConfiguration.Width;
		var height = _graphicsConfiguration.Height;
		var mode = new VideoMode(width, height);

		var title = _hostEnvironment.ApplicationName;

		return new RenderWindow(mode, title);
	}
}
