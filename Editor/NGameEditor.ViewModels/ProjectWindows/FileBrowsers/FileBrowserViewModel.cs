namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class FileBrowserViewModel : ViewModelBase
{
	public DirectoryOverviewViewModel DirectoryOverviewViewModel { get; } = new();
	public DirectoryContentViewModel DirectoryContentViewModel { get; } = new();
}
