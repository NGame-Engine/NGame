using SFML.Graphics;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using NGameTexture = NGame.Components.Renderer2Ds.Texture;

namespace NGamePlatform.Desktop.Sfml.Renderers;



public class AssetLoader
{
	public readonly Dictionary<NGameTexture, Texture> Textures = new();
	public readonly Dictionary<NGameFont, Font> Fonts = new();


	public Texture LoadTexture(NGameTexture ngTexture)
	{
		if (!Textures.ContainsKey(ngTexture))
		{
			var filename = ngTexture.FilePath;
			var texture = new Texture(filename);
			Textures.Add(ngTexture, texture);
		}

		return Textures[ngTexture];
	}


	public Font LoadFont(NGameFont ngFont)
	{
		if (!Fonts.ContainsKey(ngFont))
		{
			Fonts.Add(ngFont, new Font(ngFont.FilePath));
		}

		return Fonts[ngFont];
	}
}
