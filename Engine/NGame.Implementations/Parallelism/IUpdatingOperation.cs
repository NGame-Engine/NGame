namespace NGame.Core.Parallelism;



public interface IUpdatingOperation
{
	bool IsDone { get; }

	void Start();
	void UpdateStatus();
}
