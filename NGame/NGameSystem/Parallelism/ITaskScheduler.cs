namespace NGame.NGameSystem.Parallelism;



public interface ITaskScheduler
{
	void Run(IEnumerable<Action> actions);
	void Run<T>(IEnumerable<T> enumerable, Action<T> action);
	IEnumerable<T> AsParallel<T>(IEnumerable<T> source);
}



public static class EnumerableExtensions
{
	public static IEnumerable<T> AsParallel<T>(this IEnumerable<T> source, ITaskScheduler taskScheduler) =>
		taskScheduler.AsParallel(source);
}
