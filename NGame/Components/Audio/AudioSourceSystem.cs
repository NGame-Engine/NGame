using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Audio;



public class AudioSourceSystem : ISystem, IUpdatable
{
	private readonly IAudioPlugin _audioPlugin;
	private readonly List<Data> _datas = new();


	public AudioSourceSystem(IAudioPlugin audioPlugin)
	{
		_audioPlugin = audioPlugin;
	}


	public ICollection<Type> RequiredComponents { get; } = new List<Type>
	{
		typeof(AudioSource),
		typeof(Transform)
	};


	public void Add(Entity entity, ISet<Type> componentTypes)
	{
		if (!componentTypes.IsSupersetOf(RequiredComponents)) return;
		
		var transform = entity.GetRequiredComponent<Transform>();
		var audioSource = entity.GetRequiredComponent<AudioSource>();

		_audioPlugin.AddSource(audioSource);

		var audioClip = audioSource.AudioClip;
		if (audioClip == null) return;

		if (!_audioPlugin.IsClipLoaded(audioClip))
		{
			_audioPlugin.Load(audioClip);
		}

		var data = new Data(transform, audioSource);
		_datas.Add(data);
	}


	public void Update(GameTime gameTime)
	{
		foreach (var data in _datas)
		{
			_audioPlugin.SetSourcePosition(data.AudioSource, data.Transform.Position);
		}
	}



	private class Data
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
