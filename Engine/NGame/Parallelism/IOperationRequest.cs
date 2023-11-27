namespace NGame.Parallelism;



public interface IOperationRequest<TProgress, TResult>
{
	TProgress InitialProgress { get; }
	TResult Execute(Action<TProgress> updateProgress);
}
