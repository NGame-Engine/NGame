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

	public ObservableCollectionExtended<DirectoryContentItemViewModel> Items { get; } = [];
	public ObservableCollectionExtended<DirectoryContentItemViewModel> SelectedItems { get; } = [];
}
