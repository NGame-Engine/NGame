namespace NGame.NGameSystem.Parallelism;



public interface ITaskScheduler
{
	void Run(IEnumerable<Action> actions);
	void Run<T>(IEnumerable<T> enumerable, Action<T> action);
}
