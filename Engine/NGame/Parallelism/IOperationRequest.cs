namespace NGame.Parallelism;



public interface IOperationRequest<out TProgress, out TResult>
{
	TProgress InitialProgress { get; }
	TResult Execute(Action<TProgress> updateProgress);
}
