using NGameEditor.Functionality.Scenes;
using NGameEditor.ViewModels.ProjectWindows;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IProjectWindowViewModelFactory
{
	ProjectWindowViewModel Create();
}



public class ProjectWindowViewModelFactory(
	HierarchyViewModel hierarchy,
	InspectorViewModel inspectorViewModel,
	MenuViewModel menuViewModel,
	FileBrowserViewModel fileBrowserViewModel,
	ISceneSaver sceneSaver
) : IProjectWindowViewModelFactory
{
	public ProjectWindowViewModel Create()
	{
		var projectWindowViewModel =
			new ProjectWindowViewModel(
				hierarchy,
				inspectorViewModel,
				menuViewModel,
				fileBrowserViewModel
			)
			{
				SaveScene = ReactiveCommand.Create(sceneSaver.SaveCurrentScene)
			};


		return projectWindowViewModel;
	}
}
