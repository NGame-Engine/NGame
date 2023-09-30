using Microsoft.Extensions.Logging;

namespace NGame.Components.Audio;



public interface IGlobalSoundPlayer
{
	int AudioSourceLimit { get; set; }
	void Play(AudioClip audioClip);
}



internal class GlobalSoundPlayer : IGlobalSoundPlayer
{
	private readonly IAudioPlugin _audioPlugin;
	private readonly ILogger<GlobalSoundPlayer> _logger;
	private readonly List<AudioSource> _audioSources = new();


	public GlobalSoundPlayer(
		IAudioPlugin audioPlugin,
		ILogger<GlobalSoundPlayer> logger
	)
	{
		_audioPlugin = audioPlugin;
		_logger = logger;
	}


	public int AudioSourceLimit { get; set; } = 16;


	public void Play(AudioClip audioClip)
	{
		if (!_audioPlugin.IsClipLoaded(audioClip))
		{
			_audioPlugin.Load(audioClip);
		}

		var freeSource = GetFreeAudioSource();
		if (freeSource == null) return;

		_audioPlugin.SetSourceClip(freeSource, audioClip);
		_audioPlugin.Play(freeSource);
	}


	private AudioSource? GetFreeAudioSource()
	{
		var freeSource =
			_audioSources
				.FirstOrDefault(x => !_audioPlugin.IsPlaying(x));

		if (freeSource != null) return freeSource;


		if (_audioSources.Count >= AudioSourceLimit)
		{
			_logger.LogError(
				"Amount of global audio sources at limit ({0}), skipping sound",
				AudioSourceLimit
			);
			return null;
		}

		freeSource = new AudioSource();
		_audioPlugin.AddGlobalSource(freeSource);
		_audioSources.Add(freeSource);

		return freeSource;
	}
}
