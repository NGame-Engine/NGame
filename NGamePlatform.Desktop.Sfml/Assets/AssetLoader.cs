using NGame.Components.Audio;
using SFML.Audio;
using SFML.Graphics;
using NGameFont = NGame.Components.Renderer2Ds.Font;
using NGameTexture = NGame.Components.Renderer2Ds.Texture;

namespace NGamePlatform.Desktop.Sfml.Assets;



public interface IAssetLoader
{
	Texture LoadTexture(NGameTexture ngTexture);
	Font LoadFont(NGameFont ngFont);
	SoundBuffer LoadSoundBuffer(AudioClip audioClip);
	void DisposeBuffers();
}



public class AssetLoader : IAssetLoader
{
	private readonly Dictionary<NGameTexture, Texture> _textures = new();
	private readonly Dictionary<NGameFont, Font> _fonts = new();
	private readonly Dictionary<AudioClip, SoundBuffer> _soundBuffers = new();


	Texture IAssetLoader.LoadTexture(NGameTexture ngTexture)
	{
		if (!_textures.ContainsKey(ngTexture))
		{
			var filename = ngTexture.FilePath;
			var texture = new Texture(filename);
			_textures.Add(ngTexture, texture);
		}

		return _textures[ngTexture];
	}


	Font IAssetLoader.LoadFont(NGameFont ngFont)
	{
		if (!_fonts.ContainsKey(ngFont))
		{
			_fonts.Add(ngFont, new Font(ngFont.FilePath));
		}

		return _fonts[ngFont];
	}


	SoundBuffer IAssetLoader.LoadSoundBuffer(AudioClip audioClip)
	{
		if (!_soundBuffers.ContainsKey(audioClip))
		{
			var newSoundBuffer = new SoundBuffer(audioClip.FilePath);
			_soundBuffers.Add(audioClip, newSoundBuffer);
		}

		return _soundBuffers[audioClip];
	}


	void IAssetLoader.DisposeBuffers()
	{
		foreach (var soundBuffer in _soundBuffers.Values)
		{
			soundBuffer.Dispose();
		}
	}
}
