using NGamePlatform.Desktop.Sfml.Audio;

namespace NGamePlatform.Desktop.Sfml.Assets;



public interface IAssetDisposer
{
	void Dispose();
}



public class AssetDisposer : IAssetDisposer
{
	private readonly IAssetLoader _assetLoader;
	private readonly IGlobalSoundPlayer _globalSoundPlayer;
	private readonly AudioSourceSystem _audioSourceSystem;


	public AssetDisposer(
		IAssetLoader assetLoader,
		IGlobalSoundPlayer globalSoundPlayer,
		AudioSourceSystem audioSourceSystem
	)
	{
		_assetLoader = assetLoader;
		_globalSoundPlayer = globalSoundPlayer;
		_audioSourceSystem = audioSourceSystem;
	}


	public void Dispose()
	{
		_globalSoundPlayer.UnloadSounds();
		_audioSourceSystem.UnloadSounds();

		// SoundBuffers need to be disposed after Sounds
		_assetLoader.DisposeBuffers();
	}
}
