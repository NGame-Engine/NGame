using NGame.Assets;

namespace NGame.Services;



public interface IGlobalSoundPlayer
{
	int AudioSourceLimit { get; set; }
	void Play(AudioClip audioClip);
	void UnloadSounds();
}
