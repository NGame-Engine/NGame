namespace NGame.Parallelism;



public interface IRunningOperation<out TProgress, out TResult>
{
	// ReSharper disable once EventNeverSubscribedTo.Global
	event Action<TProgress> ProgressUpdated;
	event Action<TResult> Completed;

	TProgress Progress { get; }
	bool IsDone { get; }
	TResult? Result { get; }
}
