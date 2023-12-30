using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Shared;
using NGameEditor.Functionality.Windows;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.InterProcessCommunication;



public class FrontendApi(
	IUiThreadDispatcher uiThreadDispatcher,
	IProjectWindow projectWindow,
	ILogger<FrontendApi> logger,
	IEntityStateMapper entityStateMapper,
	SceneState sceneState
) : IFrontendApi
{
	public void UpdateLoadedScene(SceneDescription sceneDescription)
	{
		uiThreadDispatcher.DoOnUiThread(() =>
		{
			logger.LogError("FrontendApi CALLED");

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
		});
	}
}
