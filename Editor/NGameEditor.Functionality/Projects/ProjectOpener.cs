using Microsoft.Extensions.Logging;
using NGameEditor.Functionality.InterProcessCommunication;
using NGameEditor.Functionality.Windows.LauncherWindow;
using NGameEditor.Functionality.Windows.ProjectWindow;
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
		var solutionName = projectId.SolutionFilePath.NameWithoutExtension;
		projectWindow.SetProjectName(solutionName);

		projectUsageRepository.MarkProjectAsOpened(projectId);
		projectWindow.Open();


		launcherWindow.Close();
	}
}
