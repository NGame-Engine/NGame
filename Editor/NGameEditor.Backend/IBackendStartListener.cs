namespace NGameEditor.Backend;



public interface IBackendStartListener
{
	int Priority { get; }
	void OnBackendStarted();
}
