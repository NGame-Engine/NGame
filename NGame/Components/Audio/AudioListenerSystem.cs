using Microsoft.Extensions.Logging;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Audio;



public class AudioListenerSystem : ISystem, IUpdatable
{
	private readonly IAudioPlugin _audioPlugin;
	private readonly ILogger<AudioListenerSystem> _logger;
	private readonly List<Data> _datas = new();
	private bool _hasWarnedThatNoListenerExists;
	private bool _hasCheckedIfTooManyListeners;


	public AudioListenerSystem(IAudioPlugin audioPlugin, ILogger<AudioListenerSystem> logger)
	{
		_audioPlugin = audioPlugin;
		_logger = logger;
	}


	public ICollection<Type> RequiredComponents { get; } = new List<Type>
	{
		typeof(AudioListener),
		typeof(Transform)
	};


	public void Add(Entity entity)
	{
		var transform = entity.GetRequiredComponent<Transform>();
		var audioListener = entity.GetRequiredComponent<AudioListener>();

		_audioPlugin.AddAudioListener(audioListener, transform);

		var data = new Data(transform, audioListener);
		_datas.Add(data);
	}


	public void Update(GameTime gameTime)
	{
		if (_datas.Count == 0)
		{
			if (_hasWarnedThatNoListenerExists) return;

			_logger.LogWarning("No active audio listener, sounds will be global");
			_hasWarnedThatNoListenerExists = true;
			return;
		}

		if (!_hasCheckedIfTooManyListeners)
		{
			_hasCheckedIfTooManyListeners = true;
			if (_datas.Count > 1)
			{
				_logger.LogWarning(
					"More than one AudioListener in the scene, the second one will be ignored"
				);
			}
		}


		var data = _datas[0];
		_audioPlugin.SetListenerPosition(data.Transform);
	}



	private class Data
	{
		public Transform Transform;
		public AudioListener AudioListener;


		public Data(Transform transform, AudioListener audioListener)
		{
			Transform = transform;
			AudioListener = audioListener;
		}
	}
}
