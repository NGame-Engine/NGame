using System.Windows.Input;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;

namespace NGameEditor.ViewModels.ProjectWindows;



public class ProjectWindowViewModel(
	HierarchyViewModel hierarchy,
	InspectorViewModel inspectorViewModel,
	MenuViewModel menuViewModel,
	FileBrowserViewModel fileBrowserViewModel
)
	: ViewModelBase
{
	public MenuViewModel MenuViewModel { get; } = menuViewModel;
	public HierarchyViewModel Hierarchy { get; } = hierarchy;
	public InspectorViewModel InspectorViewModel { get; } = inspectorViewModel;
	public FileBrowserViewModel FileBrowserViewModel { get; } = fileBrowserViewModel;

	public ICommand? SaveScene { get; set; }
}
