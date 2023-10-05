using Microsoft.Extensions.Logging;
using NGame.Components.Audio;
using NGamePlatform.Desktop.Sfml.Assets;
using SFML.Audio;

namespace NGamePlatform.Desktop.Sfml.Audio;



public interface IGlobalSoundPlayer
{
	int AudioSourceLimit { get; set; }
	void Play(AudioClip audioClip);
	void UnloadSounds();
}



internal class GlobalSoundPlayer : IGlobalSoundPlayer
{
	private readonly ILogger<GlobalSoundPlayer> _logger;
	private readonly IAssetLoader _assetLoader;
	private readonly List<Sound> _sounds = new();


	public GlobalSoundPlayer(
		ILogger<GlobalSoundPlayer> logger,
		IAssetLoader assetLoader
	)
	{
		_logger = logger;
		_assetLoader = assetLoader;
	}


	public int AudioSourceLimit { get; set; } = 16;


	public void Play(AudioClip audioClip)
	{
		var soundBuffer = _assetLoader.LoadSoundBuffer(audioClip);

		var freeSource = GetFreeAudioSource();
		if (freeSource == null) return;

		freeSource.SoundBuffer = soundBuffer;
		freeSource.Play();
	}


	private Sound? GetFreeAudioSource()
	{
		var freeSource =
			_sounds
				.FirstOrDefault(x => x.Status == SoundStatus.Stopped);

		if (freeSource != null) return freeSource;


		if (_sounds.Count >= AudioSourceLimit)
		{
			_logger.LogError(
				"Amount of global audio sources at limit ({Limit}), skipping sound",
				AudioSourceLimit
			);
			return null;
		}

		freeSource = new Sound();
		_sounds.Add(freeSource);

		return freeSource;
	}


	public void UnloadSounds()
	{
		foreach (var sound in _sounds)
		{
			sound.Dispose();
		}
	}
}
