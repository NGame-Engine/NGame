using NGame.Components.Transforms;

namespace NGame.Components.Audio;



public interface IAudioPlugin
{
	void AddAudioListener(AudioListener audioListener, Transform transform);
	void SetListenerPosition(Transform dataTransform);
	void Load(AudioClip audioClip);
	void SetSourcePosition(AudioSource dataAudioSource, Transform dataTransform);
}
