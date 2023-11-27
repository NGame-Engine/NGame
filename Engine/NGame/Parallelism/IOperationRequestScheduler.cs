namespace NGame.Parallelism;



public interface IOperationRequestScheduler
{
	IRunningOperation<TProgress, TResult> Schedule<TProgress, TResult>(
		IOperationRequest<TProgress, TResult> operationRequest
	);
}
