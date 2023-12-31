using System.Reactive;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Windows.ProjectWindow;

namespace NGameEditor.Functionality.Projects;



public interface IExistingProjectOpener
{
	Task<Unit> OnOpenExistingProject();
}



public class ExistingExistingProjectOpener(
	IProjectOpener projectOpener,
	IProjectWindow projectWindow
) : IExistingProjectOpener
{
	public async Task<Unit> OnOpenExistingProject()
	{
		var filePaths = await projectWindow.AskUserToPickFile(
			new OpenFileOptions
			{
				Title = "Select project to open",
				AllowMultiple = false,
				FileTypeFilter = new[]
				{
					new FileType
					{
						Name = "NGame Project",
						Patterns = new[] { "*.sln" }
					}
				}
			}
		);

		var firstFilePath = filePaths.Fir st();
		var solutionFilePath = new AbsolutePath(firstFilePath);
		var projectId = new ProjectId(solutionFilePath);

		await projectOpener.OpenProject(projectId);


		return Unit.Default;
	}
}
