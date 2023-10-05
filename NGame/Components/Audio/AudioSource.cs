using NGame.Ecs;

namespace NGame.Components.Audio;



public class AudioSource : Component
{
	public event EventHandler? PlayRequested;
	public event EventHandler? PauseRequested;
	public event EventHandler? StopRequested;


	public AudioClip? AudioClip { get; set; }
	public PlayStatus PlayStatus { get; set; }


	public void Play() => PlayRequested?.Invoke(this, EventArgs.Empty);
	public void Pause() => PauseRequested?.Invoke(this, EventArgs.Empty);
	public void Stop() => StopRequested?.Invoke(this, EventArgs.Empty);
}
