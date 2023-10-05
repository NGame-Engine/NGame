using SFML.Graphics;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using NGameTexture = NGame.Components.Renderer2Ds.Texture;
namespace NGamePlatform.Desktop.Sfml.Renderers;



public class AssetLoader
{
	public readonly Dictionary<NGameTexture, Texture> Textures = new();
	public readonly Dictionary<NGameFont, Font> Fonts = new();

}
