using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Audio;



internal class AudioSourceSystem : DataListSystem<AudioSourceSystem.Data>, IUpdatable
{
	private readonly IAudioPlugin _audioPlugin;


	public AudioSourceSystem(IAudioPlugin audioPlugin)
	{
		_audioPlugin = audioPlugin;
	}


	protected override ICollection<Type> RequiredComponents { get; } =
		new[] { typeof(AudioSource) };


	protected override Data CreateDataFromEntity(Entity entity)
	{
		var transform = entity.Transform;
		var audioSource = entity.GetRequiredComponent<AudioSource>();

		_audioPlugin.AddSource(audioSource);

		var audioClip = audioSource.AudioClip;

		if (audioClip != null && !_audioPlugin.IsClipLoaded(audioClip))
		{
			_audioPlugin.Load(audioClip);
		}

		return new Data(transform, audioSource);
	}


	public void Update(GameTime gameTime)
	{
		foreach (var data in DataEntries)
		{
			_audioPlugin.SetSourcePosition(data.AudioSource, data.Transform.Position);
		}
	}



	internal class Data
	{
		public readonly Transform Transform;
		public readonly AudioSource AudioSource;


		public Data(Transform transform, AudioSource audioSource)
		{
			Transform = transform;
			AudioSource = audioSource;
		}
	}
}
