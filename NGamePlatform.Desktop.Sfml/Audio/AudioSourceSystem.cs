using NGame.Components.Audio;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.UpdateSchedulers;
using NGamePlatform.Desktop.Sfml.Assets;
using SFML.Audio;

namespace NGamePlatform.Desktop.Sfml.Audio;



public class AudioSourceSystem :
	DataListSystem<AudioSourceSystem.Data>,
	IUpdatable
{
	private readonly IAssetLoader _assetLoader;


	public AudioSourceSystem(IAssetLoader assetLoader)
	{
		_assetLoader = assetLoader;
	}


	public int Order { get; set; } = 40000;

	protected override ICollection<Type> RequiredComponents { get; } =
		new[] { typeof(AudioSource) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;
		var audioSource = entity.GetRequiredComponent<AudioSource>();

		var audioClip = audioSource.AudioClip;
		if (audioClip != null)
		{
			_assetLoader.LoadSoundBuffer(audioClip);
		}

		var sound = new Sound();
		audioSource.PlayRequested += (_, _) => sound.Play();
		audioSource.PauseRequested += (_, _) => sound.Pause();
		audioSource.StopRequested += (_, _) => sound.Stop();

		return new Data(transform, audioSource, audioClip, sound);
	}


	public void Update(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			var audioClip = data.AudioSource.AudioClip;
			if (audioClip == null) continue;

			if (data.AudioClip != audioClip)
			{
				data.Sound.SoundBuffer = _assetLoader.LoadSoundBuffer(audioClip);
				data.AudioClip = audioClip;
			}

			data.Sound.Position = data.Transform.Position.ToSfmlVector3();

			data.AudioSource.PlayStatus =
				data.Sound.Status switch
				{
					SoundStatus.Playing => PlayStatus.Playing,
					SoundStatus.Paused => PlayStatus.Paused,
					SoundStatus.Stopped => PlayStatus.Stopped,
					_ => PlayStatus.Unknown
				};
		}
	}


	public void UnloadSounds()
	{
		foreach (var dataEntry in DataEntries)
		{
			dataEntry.Sound.Dispose();
		}
	}



	public class Data
	{
		public readonly Transform Transform;
		public readonly AudioSource AudioSource;
		public AudioClip? AudioClip;
		public readonly Sound Sound;


		public Data(Transform transform, AudioSource audioSource, AudioClip? audioClip, Sound sound)
		{
			Transform = transform;
			AudioSource = audioSource;
			AudioClip = audioClip;
			Sound = sound;
		}
	}
}
