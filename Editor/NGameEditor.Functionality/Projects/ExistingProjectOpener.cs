using NGameEditor.Functionality.Windows.LauncherWindow;
using Singulink.IO;

namespace NGameEditor.Functionality.Projects;



public interface IExistingProjectOpener
{
	Task OnOpenExistingProject();
}



public class ExistingExistingProjectOpener(
	IProjectOpener projectOpener,
	ILauncherWindow launcherWindow
) : IExistingProjectOpener
{
	private static readonly string[] FilePatterns = ["*.sln"];


	public async Task OnOpenExistingProject()
	{
		var filePaths = await launcherWindow.AskUserToPickFile(
			new OpenFileOptions
			{
				Title = "Select project to open",
				AllowMultiple = false,
				FileTypeFilter = new[]
				{
					new FileType
					{
						Name = "NGame Project",
						Patterns = FilePatterns
					}
				},
				SuggestedStartLocation = null
			}
		);

		var firstFilePath = filePaths.FirstOrDefault();
		if (firstFilePath == null) return;


		var solutionFilePath = FilePath.ParseAbsolute(firstFilePath);
		var projectId = new ProjectId(solutionFilePath);

		await projectOpener.OpenProject(projectId);
	}
}
