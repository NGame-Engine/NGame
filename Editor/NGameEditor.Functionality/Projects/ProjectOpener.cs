using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.InterProcessCommunication;
using NGameEditor.Functionality.Windows;
using NGameEditor.Results;

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
	ILogger<ProjectOpener> logger
)
	: IProjectOpener
{
	public Task OpenProject(ProjectId projectId) =>
		backendStarter
			.StartBackend(projectId)
			.FinallyAsync(
				_ => OpenProjectInternal(projectId),
				logger.Log
			);


	private void OpenProjectInternal(ProjectId projectId)
	{
		var solutionFilePath = projectId.SolutionFilePath.Path;
		var solutionName = Path.GetFileNameWithoutExtension(solutionFilePath);
		projectWindow.SetProjectName(solutionName);

		projectUsageRepository.MarkProjectAsOpened(projectId);
		projectWindow.Open();


		launcherWindow.Close();
	}
}
