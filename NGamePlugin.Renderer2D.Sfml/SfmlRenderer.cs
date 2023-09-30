using Microsoft.Extensions.Logging;
using NGame.OsWindows;
using NGame.Renderers;
using SFML.Graphics;
using SFML.System;
using Font = SFML.Graphics.Font;
using NGameSprite = NGame.Sprites.Sprite;
using NGameTexture = NGame.Sprites.Texture;
using NGameTransform = NGame.Transforms.Transform;
using NGameFont = NGame.Renderers.Font;
using Sprite = SFML.Graphics.Sprite;
using Text = SFML.Graphics.Text;
using Texture = SFML.Graphics.Texture;


namespace NGamePlugin.Renderer2D.Sfml;

public class SfmlRenderer : INGameRenderer
{
	private readonly ILogger<SfmlRenderer> _logger;
	private readonly IOsWindow _window;
	private readonly GraphicsConfiguration _graphicsConfiguration;

	private readonly Dictionary<NGameTexture, Texture> _textures = new();
	private readonly Dictionary<NGameFont, Font> _fonts = new();

	private RenderTexture? _renderTexture;


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
	}


	bool INGameRenderer.BeginDraw()
	{
		_renderTexture!.Clear();
		return true;
	}


	void INGameRenderer.Draw(NGameSprite sprite, NGameTransform transform)
	{
		var texture = sprite.Texture;
		if (!_textures.ContainsKey(texture))
		{
			var filename = texture.FilePath;
			var image = new Texture(filename);
			_textures.Add(texture, image);
		}

		var spriteTexture = _textures[sprite.Texture];
		var sp = new Sprite(spriteTexture);

		sp.TextureRect = new IntRect(
			sprite.SourceRectangle.X,
			sprite.SourceRectangle.Y,
			sprite.SourceRectangle.Width,
			sprite.SourceRectangle.Height
		);

		sp.Position = new Vector2f(
			transform.Position.X + sprite.TargetRectangle.X,
			transform.Position.Y + sprite.TargetRectangle.Y
		);

		_renderTexture.Draw(sp);
	}


	void INGameRenderer.Draw(Line line)
	{
		var vertices =
			line
				.Vertices
				.Select(x => new Vertex(x.ToSfmlVector2()))
				.ToArray();


		for (int i = 0; i < vertices.Length - 1; i++)
		{
			var firstVertex = vertices[i];
			var secondVertex = vertices[i + 1];


			_renderTexture.Draw(new[] { firstVertex, secondVertex }, PrimitiveType.Lines);
		}
	}


	void INGameRenderer.Draw(NGame.Renderers.Text nGameText, NGameTransform transform)
	{
		var nGameFont = nGameText.Font;
		if (!_fonts.ContainsKey(nGameFont))
		{
			_fonts.Add(nGameFont, new Font(nGameFont.FilePath));
		}

		var font = _fonts[nGameFont];
		var text = new Text(nGameText.Content, font);
		text.CharacterSize = nGameText.CharacterSize;
		text.Origin = nGameText.TransformOrigin.ToSfmlVector2();
		text.Position = transform.Position.ToSfmlVector2();

		_renderTexture.Draw(text);
	}


	void INGameRenderer.EndDraw(bool shouldPresent)
	{
		_renderTexture.Display();

		var texture = _renderTexture.Texture;
		var image = texture.CopyToImage();
		var pixels = image.Pixels;
		_window.Draw(pixels);
	}
}
