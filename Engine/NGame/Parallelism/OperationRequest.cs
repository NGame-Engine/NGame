namespace NGame.Parallelism;



public class OperationRequest<TProgress, TResult>
	: IOperationRequest<TProgress, TResult>
{
	private readonly Func<Action<TProgress>, TResult> _workLoad;


	public OperationRequest(
		TProgress initialProgress,
		Func<Action<TProgress>, TResult> workLoad
	)
	{
		InitialProgress = initialProgress;
		_workLoad = workLoad;
	}


	public TProgress InitialProgress { get; }


	public TResult Execute(Action<TProgress> updateProgress) =>
		_workLoad.Invoke(updateProgress);
}
