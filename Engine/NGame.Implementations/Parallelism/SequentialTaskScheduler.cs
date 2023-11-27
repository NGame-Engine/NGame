using NGame.Parallelism;

namespace NGame.Core.Parallelism;



public class SequentialTaskScheduler : ITaskScheduler
{
	public void Run(IEnumerable<Action> actions)
	{
		foreach (var action in actions)
		{
			action();
		}
	}


	public void Run<T>(IEnumerable<T> enumerable, Action<T> action)
	{
		foreach (var item in enumerable)
		{
			action(item);
		}
	}


	public IEnumerable<T> AsParallel<T>(IEnumerable<T> source) => source;
}
