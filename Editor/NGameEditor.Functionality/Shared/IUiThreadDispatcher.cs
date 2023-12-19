namespace NGameEditor.Functionality.Shared;



public interface IUiThreadDispatcher
{
	void DoOnUiThread(Action action);
}
