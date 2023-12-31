using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;

namespace NGameEditor.Functionality.Windows;



public interface IProjectWindowViewModelFactory
{
	ProjectWindowViewModel Create();
}



public class ProjectWindowViewModelFactory(
	HierarchyViewModel hierarchy,
	InspectorViewModel inspectorViewModel,
	MenuViewModel menuViewModel,
	ISceneController sceneController,
	FileBrowserViewModel fileBrowserViewModel
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
				SaveScene = sceneController.SaveScene()
			};


		return projectWindowViewModel;
	}
}
