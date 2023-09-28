﻿using Microsoft.Extensions.Logging;
using NGame.OsWindows;
using NGame.Renderers;
using NGame.UpdateSchedulers;
using SFML.Graphics;
using SFML.System;

namespace NGamePlugin.Renderer2D.Sfml;

public class SfmlRenderer : INGameRenderer
{
	private readonly ILogger<SfmlRenderer> _logger;
	private readonly IOsWindow _window;


	private RenderTexture? _renderTexture;
	private Text? _text;
	private Sprite? _sprite;

	private Clock _clock = new Clock();
	private float _delta = 0f;
	private float _angle = 0f;
	private float _angleSpeed = 90f;


	public SfmlRenderer(ILogger<SfmlRenderer> logger, IOsWindow window)
	{
		_logger = logger;
		_window = window;
	}


	public void Initialize()
	{
		_logger.LogDebug("Initialize");

		_renderTexture = new RenderTexture(250, 250);

		var texture = new Texture("Images\\moon.png");
		_sprite = new Sprite(texture);

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


	public void SetPixelsPointer(IntPtr pixelsPointer)
	{
		throw new NotImplementedException();
	}


	public bool BeginDraw()
	{
		//_logger.LogInformation("BeginDraw");
		return true;
	}


	public void Draw(GameTime drawLoopTime)
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
		_window.Draw(pixels);
	}


	public void EndDraw(bool shouldPresent)
	{
		//_logger.LogInformation("EndDraw");
	}
}