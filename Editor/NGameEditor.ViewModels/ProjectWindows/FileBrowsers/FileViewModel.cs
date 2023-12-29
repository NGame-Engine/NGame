namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class FileViewModel(
	string name
) : ViewModelBase
{
	public string Name { get; } = name;
}
