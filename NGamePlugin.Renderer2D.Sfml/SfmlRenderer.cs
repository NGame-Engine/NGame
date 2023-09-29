using Microsoft.Extensions.Logging;
using NGame.OsWindows;
using NGame.Renderers;
using NGame.Sprites;
using NGame.UpdateSchedulers;
using SFML.Graphics;
using SFML.System;
using NGameSprite = NGame.Sprites.Sprite;
using NGameTexture = NGame.Sprites.Texture;
using Sprite = SFML.Graphics.Sprite;
using Texture = SFML.Graphics.Texture;


namespace NGamePlugin.Renderer2D.Sfml;

public class SfmlRenderer : INGameRenderer
{
	private readonly ILogger<SfmlRenderer> _logger;
	private readonly IOsWindow _window;
	private readonly GraphicsConfiguration _graphicsConfiguration;

	private RenderTexture? _renderTexture;
	private Text? _text;


	public SfmlRenderer(ILogger<SfmlRenderer> logger, IOsWindow window, GraphicsConfiguration graphicsConfiguration)
	{
		_logger = logger;
		_window = window;
		_graphicsConfiguration = graphicsConfiguration;
	}


	void INGameRenderer.Initialize()
	{
		_logger.LogDebug("Initialize");

		var width = _graphicsConfiguration.Width;
		var height = _graphicsConfiguration.Height;
		_renderTexture = new RenderTexture(width, height);

		var font = new Font("Fonts/YanoneKaffeesatz-VariableFont_wght.ttf");
		_text = new Text("Hello World!", font);
		_text.CharacterSize = 40;
		var textWidth = _text.GetLocalBounds().Width;
		var textHeight = _text.GetLocalBounds().Height;
		var xOffset = _text.GetLocalBounds().Left;
		var yOffset = _text.GetLocalBounds().Top;
		_text.Origin = new Vector2f(textWidth / 2f + xOffset, textHeight / 2f + yOffset);
		_text.Position = new Vector2f(_renderTexture.Size.X / 2f, _renderTexture.Size.Y / 2f);
	}


	private readonly Dictionary<NGameTexture, Texture> _textures = new();
	private readonly List<RendererSprite> _sprites = new();


	void INGameRenderer.Add(RendererSprite sprite)
	{
		var texture = sprite.Texture;
		if (!_textures.ContainsKey(texture))
		{
			var filename = texture.FilePath;
			var image = new Texture(filename);
			_textures.Add(texture, image);
		}

		_sprites.Add(sprite);
	}


	bool INGameRenderer.BeginDraw()
	{
		//_logger.LogInformation("BeginDraw");
		return true;
	}


	void INGameRenderer.Draw(GameTime drawLoopTime)
	{
		_renderTexture!.Clear();

		foreach (var sprite in _sprites)
		{
			var spriteTexture = _textures[sprite.Texture];
			var sp = new Sprite(spriteTexture);

			sp.TextureRect = new IntRect(
				sprite.SourceRectangle.X,
				sprite.SourceRectangle.Y,
				sprite.SourceRectangle.Width,
				sprite.SourceRectangle.Height
			);

			sp.Position = new Vector2f(sprite.TargetRectangle.X, sprite.TargetRectangle.Y);
			

			_renderTexture.Draw(sp);
		}

		_renderTexture.Display();

		var texture = _renderTexture.Texture;
		var image = texture.CopyToImage();
		var pixels = image.Pixels;
		_window.Draw(pixels);
	}


	void INGameRenderer.EndDraw(bool shouldPresent)
	{
		//_logger.LogInformation("EndDraw");
	}
}
