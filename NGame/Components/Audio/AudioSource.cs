using NGame.Ecs;

namespace NGame.Components.Audio;



public class AudioSource : IComponent
{
	public AudioClip? AudioClip { get; set; }
}
