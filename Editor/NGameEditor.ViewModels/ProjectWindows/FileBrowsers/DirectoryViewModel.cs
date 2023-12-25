using DynamicData.Binding;
using NGameEditor.ViewModels.Components.Menus;

namespace NGameEditor.ViewModels.ProjectWindows.FileBrowsers;



public class DirectoryViewModel(
	string name,
	ContextMenuViewModel contextMenu
) : ViewModelBase
{
	public string Name { get; } = name;
	public string DisplayName => $"📁 {Name}";
	
	public ObservableCollectionExtended<DirectoryViewModel> Directories { get; } = new();
	public ObservableCollectionExtended<FileViewModel> Files { get; } = new();

	public ContextMenuViewModel ContextMenu { get; } = contextMenu;
}
