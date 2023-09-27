using Microsoft.Extensions.Logging;
using NGame.Renderers;
using NGame.UpdaterSchedulers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace NGamePlugin.Sfml;

public class NGameRenderer : INGameRenderer
{
	private readonly ILogger<NGameRenderer> _logger;


	public NGameRenderer(ILogger<NGameRenderer> logger)
	{
		_logger = logger;
	}


	private RenderWindow? _window;
	private Text? _text;
	private Sprite _sprite;

	private Clock _clock = new Clock();
	private float _delta = 0f;
	private float _angle = 0f;
	private float _angleSpeed = 90f;


	public void Initialize()
	{
		_logger.LogDebug("Initialize");
	}


	public void InitializeOld()
	{
		VideoMode mode = new VideoMode(250, 250);
		_window = new RenderWindow(mode, "SFML.NET");

		_window.Closed += (obj, e) => { _window.Close(); };
		_window.KeyPressed +=
			(sender, e) =>
			{
				Window window = (Window)sender;
				if (e.Code == Keyboard.Key.Escape)
				{
					window.Close();
				}
			};

		Texture texture = new Texture("Images\\moon.png");


		_sprite = new Sprite(texture);


		Font font = new Font("C:/Windows/Fonts/arial.ttf");
		_text = new Text("Hello World!", font);
		_text.CharacterSize = 40;
		float textWidth = _text.GetLocalBounds().Width;
		float textHeight = _text.GetLocalBounds().Height;
		float xOffset = _text.GetLocalBounds().Left;
		float yOffset = _text.GetLocalBounds().Top;
		_text.Origin = new Vector2f(textWidth / 2f + xOffset, textHeight / 2f + yOffset);
		_text.Position = new Vector2f(_window.Size.X / 2f, _window.Size.Y / 2f);
	}


	public Task<bool> BeginDraw()
	{
		//_logger.LogInformation("BeginDraw");
		return Task.FromResult(true);
	}


	public Task Draw(GameTime drawLoopTime)
	{
		return Task.CompletedTask;
	}


	public Task DrawOld(GameTime drawLoopTime)
	{
		_delta = _clock.Restart().AsSeconds();
		_angle += _angleSpeed * _delta;
		_window!.DispatchEvents();
		_window.Clear();
		_text!.Rotation = _angle;
		_window.Draw(_text);
		_window.Draw(_sprite);
		_window.Display();

		return Task.CompletedTask;
	}


	public Task EndDraw(bool shouldPresent)
	{
		//_logger.LogInformation("EndDraw");
		return Task.CompletedTask;
	}
}
