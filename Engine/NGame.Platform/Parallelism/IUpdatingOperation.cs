namespace NGame.Platform.Parallelism;



public interface IUpdatingOperation
{
	bool IsDone { get; }

	void Start();
	void UpdateStatus();
}
