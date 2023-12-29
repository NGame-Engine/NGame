using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Windows;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
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
	SceneState sceneState,
	FileBrowserViewModel fileBrowserViewModel
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


		UpdateProjectFiles(backendService);


		launcherWindow.Close();
	}


	private void UpdateProjectFiles(IBackendService backendService)
	{
		backendService
			.GetProjectFiles()
			.Then(x =>
			{
				fileBrowserViewModel.DirectoryOverviewViewModel.Directories.Clear();
				fileBrowserViewModel
					.DirectoryOverviewViewModel
					.Directories
					.AddRange(
						x
							.SubDirectories
							.Select(MapDirectoryDescriptionRecursively)
					);
			})
			.IfError(logger.Log);
	}


	private DirectoryViewModel MapDirectoryDescriptionRecursively(
		DirectoryDescription directoryDescription
	)
	{
		var directoryViewModel =
			new DirectoryViewModel(
				directoryDescription.Name,
				new ContextMenuViewModel([])
			);

		foreach (var subDirectory in directoryDescription.SubDirectories)
		{
			directoryViewModel
				.Directories
				.Add(MapDirectoryDescriptionRecursively(subDirectory));
		}

		foreach (var fileDescription in directoryDescription.Files)
		{
			directoryViewModel
				.Files
				.Add(new FileViewModel(fileDescription.Name));
		}

		return directoryViewModel;
	}
}
