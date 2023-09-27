using NGame.Renderers;
using SFML.Graphics;

namespace NGamePlugin.Renderer2D.Sfml;

public class SfmlRenderTexture : IRenderTexture
{
	private byte[]? _pixels;


	public void SetPixels(byte[] pixels)
	{
		_pixels = pixels;
	}


	public byte[] GetPixels()
	{
		return _pixels!;
	}
}
