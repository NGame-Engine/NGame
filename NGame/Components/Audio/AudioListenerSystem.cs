using Microsoft.Extensions.Logging;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Audio;



public class AudioListenerSystem : DataListSystem<AudioListenerSystem.Data>, IUpdatable
{
	private readonly IAudioPlugin _audioPlugin;
	private readonly ILogger<AudioListenerSystem> _logger;


	public AudioListenerSystem(IAudioPlugin audioPlugin, ILogger<AudioListenerSystem> logger)
	{
		_audioPlugin = audioPlugin;
		_logger = logger;
	}


	private bool _hasWarnedThatNoListenerExists;
	private bool _hasCheckedIfTooManyListeners;


	protected override ICollection<Type> RequiredComponents { get; } =
		new[] { typeof(AudioListener) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;
		var audioListener = entity.GetRequiredComponent<AudioListener>();

		_audioPlugin.AddAudioListener(audioListener, transform);

		return new Data(transform, audioListener);
	}


	public void Update(GameTime gameTime)
	{
		if (DataEntries.Count == 0)
		{
			if (_hasWarnedThatNoListenerExists) return;

			_logger.LogWarning("No active audio listener, sounds will be global");
			_hasWarnedThatNoListenerExists = true;
			return;
		}

		if (!_hasCheckedIfTooManyListeners)
		{
			_hasCheckedIfTooManyListeners = true;
			if (DataEntries.Count > 1)
			{
				_logger.LogWarning(
					"More than one AudioListener in the scene, the second one will be ignored"
				);
			}
		}


		var data = DataEntries.First();
		_audioPlugin.SetListenerPosition(data.Transform);
	}



	public class Data
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
