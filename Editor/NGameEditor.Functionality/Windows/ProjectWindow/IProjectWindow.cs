namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IProjectWindow
{
	event Action Closing;

	bool IsActive { get; }

	void Open();
	void SetProjectName(string projectName);
	void SetSceneName(string sceneFileName);
	void Close();
}
