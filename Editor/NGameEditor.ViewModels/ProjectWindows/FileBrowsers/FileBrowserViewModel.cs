namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class FileBrowserViewModel(
	DirectoryOverviewViewModel directoryOverviewViewModel,
	DirectoryContentViewModel directoryContentViewModel
) : ViewModelBase
{
	public DirectoryOverviewViewModel DirectoryOverviewViewModel { get; } = directoryOverviewViewModel;
	public DirectoryContentViewModel DirectoryContentViewModel { get; } = directoryContentViewModel;
}
