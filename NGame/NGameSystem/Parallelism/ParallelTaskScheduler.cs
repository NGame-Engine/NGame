namespace NGame.NGameSystem.Parallelism;



public class ParallelTaskScheduler : ITaskScheduler
{
	public void Run(IEnumerable<Action> actions)
	{
		Parallel.ForEach(actions, x => x());
	}


	public void Run<T>(IEnumerable<T> enumerable, Action<T> action)
	{
		Parallel.ForEach(enumerable, action);
	}
}
