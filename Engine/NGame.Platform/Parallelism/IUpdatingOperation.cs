namespace NGame.Implementations.Parallelism;



public interface IUpdatingOperation
{
	bool IsDone { get; }

	void Start();
	void UpdateStatus();
}
