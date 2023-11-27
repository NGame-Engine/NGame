namespace NGame.Parallelism;



public interface IRunningOperation<TProgress, TResult>
{
	event Action<TProgress> ProgressUpdated;
	event Action<TResult> Completed;

	TProgress Progress { get; }
	bool IsDone { get; }
	TResult? Result { get; }
}
