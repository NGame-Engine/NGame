using System.Reactive;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.ViewModels.LauncherWindows;
using NGameEditor.ViewModels.Shared;

namespace NGameEditor.Functionality.Projects;



public interface IExistingProjectOpener
{
	Task<Unit> OnOpenExistingProject(OpenExistingProjectArgs openExistingProjectArgs);
}



public class ExistingExistingProjectOpener(
	IProjectOpener projectOpener
) : IExistingProjectOpener
{
	public async Task<Unit> OnOpenExistingProject(OpenExistingProjectArgs openExistingProjectArgs)
	{
		var filePicker = openExistingProjectArgs.FilePicker;

		var filePaths = await filePicker.AskUserToPickFile(
			new IFilePicker.OpenOptions
			{
				Title = "Select project to open",
				AllowMultiple = false,
				FileTypeFilter = new[]
				{
					new IFilePicker.FileType
					{
						Name = "NGame Project",
						Patterns = new[] { "*.sln" }
					}
				}
			}
		);

		var firstFilePath = filePaths.First();
		var solutionFilePath = new AbsolutePath(firstFilePath);
		var projectId = new ProjectId(solutionFilePath);

		await projectOpener.OpenProject(projectId);


		return Unit.Default;
	}
}
