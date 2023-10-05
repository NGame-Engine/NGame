using Microsoft.Extensions.Options;
using SFML.Graphics;

namespace NGamePlatform.Desktop.Sfml.Renderers;



internal class RenderTextureFactory
{
	private readonly SfmlDesktopConfiguration _sfmlDesktopConfiguration;


	public RenderTextureFactory(IOptions<SfmlDesktopConfiguration> sfmlDesktopConfiguration)
	{
		_sfmlDesktopConfiguration = sfmlDesktopConfiguration.Value;
	}


	public RenderTexture Create()
	{
		var width = (uint)_sfmlDesktopConfiguration.Width;
		var height = (uint)_sfmlDesktopConfiguration.Height;
		return new RenderTexture(width, height);
	}
}
