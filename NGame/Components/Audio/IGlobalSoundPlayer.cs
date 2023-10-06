namespace NGame.Components.Audio;



public interface IGlobalSoundPlayer
{
	int AudioSourceLimit { get; set; }
	void Play(AudioClip audioClip);
	void UnloadSounds();
}
