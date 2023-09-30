using NGame.Components.Transforms;

namespace NGame.Components.Audio;



public interface IAudioPlugin
{
	void Load(AudioClip audioClip);
	bool IsClipLoaded(AudioClip audioClip);
	void Unload(AudioClip audioClip);
	void UnloadAllClips();
	
	void AddAudioListener(AudioListener audioListener, Transform transform);
	void SetListenerPosition(Transform transform);

	void AddGlobalSource(AudioSource audioSource);
	void AddSource(AudioSource audioSource);
	bool DoesAudioSourceExist(AudioSource audioSource);
	void SetSourceClip(AudioSource audioSource, AudioClip audioClip);
	void SetSourcePosition(AudioSource audioSource, Transform transform);
	void RemoveSource(AudioSource audioSource);
	void RemoveAllSources();


	void Play(AudioSource audioSource);
	bool IsPlaying(AudioSource audioSource);
}
