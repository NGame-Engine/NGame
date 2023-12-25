using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class DirectoryContentViewModel : ViewModelBase
{
	public string DirectoryName { get; set; } = "";
	public ObservableCollectionExtended<DirectoryViewModel> Directories { get; } = new();
	public ObservableCollectionExtended<FileViewModel> Files { get; } = new();
}
