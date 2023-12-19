using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Windows;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Projects;



public interface IProjectOpener
{
	Task OpenProject(ProjectId projectId);
}



public class ProjectOpener(
	IProjectUsageRepository projectUsageRepository,
	IBackendRunner backendRunner,
	ILauncherWindow launcherWindow,
	IProjectWindow projectWindow,
	ILogger<ProjectOpener> logger,
	IEntityStateMapper entityStateMapper,
	SceneState sceneState
)
	: IProjectOpener
{
	public Task OpenProject(ProjectId projectId) =>
		backendRunner
			.StartBackend(projectId)
			.FinallyAsync(
				x => OpenProject(x, projectId),
				logger.Log
			);


	private void OpenProject(IBackendService backendService, ProjectId projectId)
	{
		var solutionFilePath = projectId.SolutionFilePath.Path;
		var solutionName = Path.GetFileNameWithoutExtension(solutionFilePath);
		projectWindow.SetProjectName(solutionName);

		projectUsageRepository.MarkProjectAsOpened(projectId);
		projectWindow.Open();


		var sceneDescription = backendService.GetLoadedScene();
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


		launcherWindow.Close();
	}
}
