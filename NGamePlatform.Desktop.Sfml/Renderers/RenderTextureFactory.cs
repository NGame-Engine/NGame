using NGame.Renderers;
using SFML.Graphics;

namespace NGamePlatform.Desktop.Sfml.Renderers;



internal class RenderTextureFactory
{
	private readonly GraphicsConfiguration _graphicsConfiguration;


	public RenderTextureFactory(GraphicsConfiguration graphicsConfiguration)
	{
		_graphicsConfiguration = graphicsConfiguration;
	}


	public RenderTexture Create()
	{
		var width = (uint)_graphicsConfiguration.Width;
		var height = (uint)_graphicsConfiguration.Height;
		return new RenderTexture(width, height);
	}
}
