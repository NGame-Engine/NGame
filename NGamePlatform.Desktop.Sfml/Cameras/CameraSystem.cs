using Microsoft.Extensions.Logging;
using NGame.Components.Cameras;
using NGame.Ecs;
using NGame.UpdateSchedulers;
using SFML.Graphics;
using SFML.System;
using Transform = NGame.Components.Transforms.Transform;

namespace NGamePlatform.Desktop.Sfml.Cameras;



public class CameraSystem : ISystem, IUpdatable
{
	private readonly ILogger<CameraSystem> _logger;
	private readonly RenderWindow _window;


	public CameraSystem(ILogger<CameraSystem> logger, RenderWindow window)
	{
		_logger = logger;
		_window = window;
	}


	private Data? _data;


	public bool EntityIsMatch(IEnumerable<Type> componentTypes) =>
		componentTypes.Any(x => x.IsAssignableTo(typeof(Camera2D)));


	public void Add(Entity entity)
	{
		if (_data != null)
		{
			_logger.LogWarning("Multiple cameras not supported, will be ignored");
			return;
		}

		var transform = entity.Transform;
		var camera2D = entity.GetRequiredComponent<Camera2D>();

		var position = new Vector2f(_window.Size.X / 2, -_window.Size.Y / 2);
		var size = camera2D.Size.ToSfmlVector2();
		var view = new View(position, size);
		_window.SetView(view);

		_data = new Data(transform, camera2D, view);
	}


	public void Remove(Entity entity)
	{
		_data = null;
	}


	public bool Contains(Entity entity) =>
		entity.Transform == _data?.Transform;


	public void Update(GameTime gameTime)
	{
		if (_data == null) return;

		var position = _data.Transform.Position * 128;
		_data.View.Center = position.ToSfmlVector2YInverted();
		_data.View.Size = _data.Camera2D.Size.ToSfmlVector2();
		_window.SetView(_data.View);
	}



	private class Data
	{
		public readonly Transform Transform;
		public readonly Camera2D Camera2D;
		public readonly View View;


		public Data(Transform transform, Camera2D camera2D, View view)
		{
			Transform = transform;
			Camera2D = camera2D;
			View = view;
		}
	}
}
