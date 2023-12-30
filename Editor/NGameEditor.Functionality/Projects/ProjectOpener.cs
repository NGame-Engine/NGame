using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.InterProcessCommunication;
using NGameEditor.Functionality.Windows;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Functionality.Projects;



public interface IProjectOpener
{
	Task OpenProject(ProjectId projectId);
}



public class ProjectOpener(
	IProjectUsageRepository projectUsageRepository,
	IBackendStarter backendStarter,
	ILauncherWindow launcherWindow,
	IProjectWindow projectWindow,
	ILogger<ProjectOpener> logger,
	FileBrowserViewModel fileBrowserViewModel
)
	: IProjectOpener
{
	public Task OpenProject(ProjectId projectId) =>
		backendStarter
			.StartBackend(projectId)
			.FinallyAsync(
				x => OpenProject(x, projectId),
				logger.Log
			);


	private void OpenProject(IBackendApi backendApi, ProjectId projectId)
	{
		var solutionFilePath = projectId.SolutionFilePath.Path;
		var solutionName = Path.GetFileNameWithoutExtension(solutionFilePath);
		projectWindow.SetProjectName(solutionName);

		projectUsageRepository.MarkProjectAsOpened(projectId);
		projectWindow.Open();


		/*
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
		*/


		UpdateProjectFiles(backendApi);


		launcherWindow.Close();
	}


	private void UpdateProjectFiles(IBackendApi backendApi)
	{
		backendApi
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
