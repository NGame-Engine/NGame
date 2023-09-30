using NGame.Components.Audio;
using NGame.Components.Transforms;
using SFML.Audio;

namespace NGamePlugin.Audio.Sfml;



public class SfmlAudioPlugin : IAudioPlugin
{
	private readonly Dictionary<AudioClip, SoundBuffer> _soundBuffers = new();
	private readonly Dictionary<AudioSource, Sound> _sounds = new();


	void IAudioPlugin.Load(AudioClip audioClip)
	{
		if (!_soundBuffers.ContainsKey(audioClip))
		{
			var newSoundBuffer = new SoundBuffer(audioClip.FilePath);
			_soundBuffers[audioClip] = newSoundBuffer;
		}
	}


	bool IAudioPlugin.IsClipLoaded(AudioClip audioClip) =>
		_soundBuffers.ContainsKey(audioClip);


	void IAudioPlugin.Unload(AudioClip audioClip)
	{
		var soundBuffer = _soundBuffers[audioClip];
		_soundBuffers.Remove(audioClip);
		soundBuffer.Dispose();
	}


	void IAudioPlugin.UnloadAllClips()
	{
		var soundBuffers = _soundBuffers.Values.ToList();
		_soundBuffers.Clear();

		foreach (var soundBuffer in soundBuffers)
		{
			soundBuffer.Dispose();
		}
	}


	void IAudioPlugin.AddAudioListener(AudioListener audioListener, Transform transform)
	{
		Listener.Position = transform.Position.ToSfmlVector3();
	}


	void IAudioPlugin.SetListenerPosition(Transform transform)
	{
		Listener.Position = transform.Position.ToSfmlVector3();
	}


	void IAudioPlugin.AddGlobalSource(AudioSource audioSource)
	{
		var sound = new Sound();
		sound.RelativeToListener = true;
		_sounds[audioSource] = sound;
	}


	void IAudioPlugin.AddSource(AudioSource audioSource)
	{
		var newSound = new Sound();
		_sounds[audioSource] = newSound;
	}


	bool IAudioPlugin.DoesAudioSourceExist(AudioSource audioSource) =>
		_sounds.ContainsKey(audioSource);


	void IAudioPlugin.SetSourceClip(AudioSource audioSource, AudioClip audioClip)
	{
		var sound = _sounds[audioSource];
		sound.SoundBuffer = _soundBuffers[audioClip];
	}


	void IAudioPlugin.SetSourcePosition(AudioSource audioSource, Transform transform)
	{
		var sound = _sounds[audioSource];
		sound.Position = transform.Position.ToSfmlVector3();
	}


	void IAudioPlugin.RemoveSource(AudioSource audioSource)
	{
		var sound = _sounds[audioSource];
		_sounds.Remove(audioSource);
		sound.Dispose();
	}


	void IAudioPlugin.RemoveAllSources()
	{
		var sounds = _sounds.Values.ToList();
		_sounds.Clear();

		foreach (var sound in sounds)
		{
			sound.Dispose();
		}
	}


	void IAudioPlugin.Play(AudioSource audioSource)
	{
		var sound = _sounds[audioSource];
		sound.Play();
	}


	public bool IsPlaying(AudioSource audioSource)
	{
		var sound = _sounds[audioSource];
		return sound.Status == SoundStatus.Playing;
	}
}
