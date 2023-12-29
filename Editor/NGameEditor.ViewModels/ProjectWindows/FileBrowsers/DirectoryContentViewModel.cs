using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class DirectoryContentViewModel : ViewModelBase
{
	private string _directoryName = "";

	public string DirectoryName
	{
		get => _directoryName;
		set => this.RaiseAndSetIfChanged(ref _directoryName, value);
	}

	public ObservableCollectionExtended<DirectoryViewModel> Directories { get; } = new();
	public ObservableCollectionExtended<FileViewModel> Files { get; } = new();
}
