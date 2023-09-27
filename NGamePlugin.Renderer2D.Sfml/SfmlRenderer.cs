using Microsoft.Extensions.Logging;
using NGame.Renderers;
using NGame.UpdateSchedulers;
using SFML.Graphics;
using SFML.System;

namespace NGamePlugin.Renderer2D.Sfml;

public class SfmlRenderer : INGameRenderer
{
	private readonly ILogger<SfmlRenderer> _logger;
	private readonly IRenderTexture _nGameRenderTexture;


	public SfmlRenderer(ILogger<SfmlRenderer> logger, IRenderTexture nGameRenderTexture)
	{
		_logger = logger;
		_nGameRenderTexture = nGameRenderTexture;
	}


	private RenderTexture? _renderTexture;
	private Text? _text;
	private Sprite? _sprite;

	private Clock _clock = new Clock();
	private float _delta = 0f;
	private float _angle = 0f;
	private float _angleSpeed = 90f;


	public void Initialize()
	{
		_logger.LogDebug("Initialize");

		_renderTexture = new RenderTexture(250, 250);

		var texture = new Texture("Images\\moon.png");
		_sprite = new Sprite(texture);

		var font = new Font("C:/Windows/Fonts/arial.ttf");
		_text = new Text("Hello World!", font);
		_text.CharacterSize = 40;
		var textWidth = _text.GetLocalBounds().Width;
		var textHeight = _text.GetLocalBounds().Height;
		var xOffset = _text.GetLocalBounds().Left;
		var yOffset = _text.GetLocalBounds().Top;
		_text.Origin = new Vector2f(textWidth / 2f + xOffset, textHeight / 2f + yOffset);
		_text.Position = new Vector2f(_renderTexture.Size.X / 2f, _renderTexture.Size.Y / 2f);
	}


	public Task<bool> BeginDraw()
	{
		//_logger.LogInformation("BeginDraw");
		return Task.FromResult(true);
	}


	public Task Draw(GameTime drawLoopTime)
	{
		_delta = _clock.Restart().AsSeconds();
		_angle += _angleSpeed * _delta;

		_renderTexture!.Clear();
		_text!.Rotation = _angle;
		_renderTexture.Draw(_text);
		_renderTexture.Draw(_sprite);

		_renderTexture.Display();

		var texture = _renderTexture.Texture;
		var image = texture.CopyToImage();
		var pixels = image.Pixels;
		_nGameRenderTexture.SetPixels(pixels);

		return Task.CompletedTask;
	}


	public Task EndDraw(bool shouldPresent)
	{
		//_logger.LogInformation("EndDraw");
		return Task.CompletedTask;
	}
}
