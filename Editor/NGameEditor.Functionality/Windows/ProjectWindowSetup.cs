using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Windows;



public interface IProjectWindowSetup
{
	void OnApplicationStart();
}



public class ProjectWindowSetup(
	HierarchyViewModel hierarchyViewModel,
	ISceneController sceneController,
	SceneState sceneState,
	SelectedEntitiesState selectedEntitiesState,
	IEntityNodeViewModelMapper entityNodeViewModelMapper
) : IProjectWindowSetup
{
	public void OnApplicationStart()
	{
		//hierarchyViewModel.AddEntity = ReactiveCommand.Create(() => sceneController.CreateEntity(null));
	}
}
