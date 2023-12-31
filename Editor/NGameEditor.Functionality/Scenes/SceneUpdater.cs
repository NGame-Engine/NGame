using NGameEditor.Bridge.Scenes;
using NGameEditor.Functionality.Windows.ProjectWindow;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Scenes;

public interface ISceneUpdater
{
	void UpdateLoadedScene(SceneDescription sceneDescription);
}



public class SceneUpdater(
	IProjectWindow projectWindow,
	IEntityStateMapper entityStateMapper,
	SceneState sceneState
) : ISceneUpdater
{
	public void UpdateLoadedScene(SceneDescription sceneDescription)
	{
		var sceneFileName = sceneDescription.FileName;
		projectWindow.SetSceneName(sceneFileName ?? "*");


		sceneState.RemoveAllEntities();

		var entityNodeViewModels = sceneDescription
			.Entities
			.Select(entityStateMapper.Map);

		foreach (var entityNodeViewModel in entityNodeViewModels)
		{
			sceneState.SceneEntities.Add(entityNodeViewModel);
		}
	}
}
