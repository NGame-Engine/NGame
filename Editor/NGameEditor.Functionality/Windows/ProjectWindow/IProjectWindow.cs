namespace NGameEditor.Functionality.Windows.ProjectWindow;



public class OpenFileOptions
{
	public string? Title { get; init; }
	public string? SuggestedStartLocation { get; init; }
	public bool AllowMultiple { get; init; }
	public IReadOnlyList<FileType>? FileTypeFilter { get; init; }
}



public sealed class FileType
{
	public string Name { get; init; } = "";
	public IReadOnlyList<string>? Patterns { get; init; }
}



public class OpenFolderOptions
{
	public string? Title { get; init; }
	public string? SuggestedStartLocation { get; init; }
	public bool AllowMultiple { get; init; }
}



public interface IProjectWindow
{
	event Action Closing;

	bool IsActive { get; }

	void Open();
	void SetProjectName(string projectName);
	void SetSceneName(string sceneFileName);
	Task<IReadOnlyList<string>> AskUserToPickFile(OpenFileOptions openFileOptions);
	Task<IReadOnlyList<string>> AskUserToPickFolder(OpenFolderOptions openFolderOptions);
	void Close();
}
