using NGame.Parallelism;

namespace NGame.Core.Parallelism;



public class ParallelTaskScheduler : ITaskScheduler
{
	void ITaskScheduler.Run(IEnumerable<Action> actions)
	{
		Parallel.ForEach(actions, x => x());
	}


	void ITaskScheduler.Run<T>(IEnumerable<T> enumerable, Action<T> action)
	{
		Parallel.ForEach(enumerable, action);
	}


	IEnumerable<T> ITaskScheduler.AsParallel<T>(IEnumerable<T> source) =>
		source.AsParallel();
}
