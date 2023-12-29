using System.Windows.Input;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Results;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;
using ReactiveUI;

namespace NGameEditor.Functionality.Controllers;



public class SceneController(
	IBackendRunner backendRunner,
	IEntityStateMapper entityStateMapper,
	ISceneSaver sceneSaver,
	SceneState sceneState,
	ILogger<SceneController> logger
) : ISceneController
{
	public Result CreateEntity(EntityState? parentEntity) =>
		backendRunner
			.GetBackendService()
			.Then(x => x.AddEntity(parentEntity?.Id))
			.Then(entityStateMapper.Map)
			.Then(x =>
			{
				if (parentEntity != null) parentEntity.Children.Add(x);
				else sceneState.SceneEntities.Add(x);
			})
			.IfError(logger.Log);


	public Result Remove(EntityState entityState) =>
		backendRunner
			.GetBackendService()
			.Then(x => x.RemoveEntity(entityState.Id))
			.Then(() =>
				sceneState
					.SceneEntities
					.FindCollectionWithEntity(entityState.Id)!
					.Remove(entityState)
			)
			.IfError(logger.Log);


	public ICommand SaveScene() =>
		ReactiveCommand.Create(sceneSaver.SaveCurrentScene);
}
