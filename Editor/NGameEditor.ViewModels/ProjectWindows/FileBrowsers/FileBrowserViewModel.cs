using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class FileBrowserViewModel : ViewModelBase
{
	public ObservableCollectionExtended<DirectoryViewModel> Directories { get; } = new();
	public ObservableCollectionExtended<FileViewModel> Files { get; } = new();

	public ObservableCollectionExtended<DirectoryViewModel> SelectedDirectories { get; } = new();

	public DirectoryOverviewViewModel DirectoryOverviewViewModel { get; } = new();
	public DirectoryContentViewModel DirectoryContentViewModel { get; } = new();
}
