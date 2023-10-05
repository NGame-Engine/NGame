using Microsoft.Extensions.Logging;
using NGame.Components.Audio;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.UpdateSchedulers;
using SFML.Audio;

namespace NGamePlatform.Desktop.Sfml.Audio;



public class AudioListenerSystem : ISystem, IUpdatable
{
	private readonly ILogger<AudioListenerSystem> _logger;


	public AudioListenerSystem(ILogger<AudioListenerSystem> logger)
	{
		_logger = logger;
	}


	public int Order { get; set; } = 40100;

	private Data? _data;
	private bool _hasWarnedThatNoListenerExists;


	public bool EntityIsMatch(IEnumerable<Type> componentTypes) =>
		componentTypes.Any(x => x.IsAssignableTo(typeof(AudioListener)));


	public void Add(Entity entity)
	{
		if (_data != null)
		{
			_logger.LogWarning(
				"More than one AudioListener in the scene, the second one will be ignored"
			);
			return;
		}


		var transform = entity.Transform;
		var audioListener = entity.GetRequiredComponent<AudioListener>();

		_data = new Data(transform, audioListener);
	}


	public void Remove(Entity entity) => _data = null;


	public bool Contains(Entity entity) =>
		entity.GetComponent<AudioListener>() == _data?.AudioListener;


	public void Update(GameTime gameTime)
	{
		if (_data == null)
		{
			if (_hasWarnedThatNoListenerExists) return;

			_logger.LogWarning("No active audio listener, sounds will be global");
			_hasWarnedThatNoListenerExists = true;
			return;
		}

		Listener.Position = _data.Transform.Position.ToSfmlVector3();
	}



	private class Data
	{
		public readonly Transform Transform;
		public readonly AudioListener AudioListener;


		public Data(Transform transform, AudioListener audioListener)
		{
			Transform = transform;
			AudioListener = audioListener;
		}
	}
}
