using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SFML.Graphics;
using SFML.Window;

namespace NGamePlatform.Desktop.Sfml.Window;



internal class RenderWindowFactory
{
	private readonly IHostEnvironment _hostEnvironment;
	private readonly SfmlDesktopConfiguration _sfmlDesktopConfiguration;


	public RenderWindowFactory(
		IHostEnvironment hostEnvironment,
		IOptions<SfmlDesktopConfiguration> configuration
	)
	{
		_hostEnvironment = hostEnvironment;
		_sfmlDesktopConfiguration = configuration.Value;
	}


	public RenderWindow Create()
	{
		var width = (uint)_sfmlDesktopConfiguration.Width;
		var height = (uint)_sfmlDesktopConfiguration.Height;
		var mode = new VideoMode(width, height);

		var title = _hostEnvironment.ApplicationName;

		return new RenderWindow(mode, title);
	}
}
