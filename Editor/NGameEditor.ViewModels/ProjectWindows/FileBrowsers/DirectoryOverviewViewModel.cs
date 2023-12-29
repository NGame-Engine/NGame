using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class DirectoryOverviewViewModel : ViewModelBase
{
	public ObservableCollectionExtended<DirectoryViewModel> Directories { get; } = new();
	public ObservableCollectionExtended<DirectoryViewModel> SelectedDirectories { get; } = new();
}
