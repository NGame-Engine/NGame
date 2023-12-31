using System.Reactive;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Projects;
using NGameEditor.Results;
using NGameEditor.ViewModels.LauncherWindows;
using NGameEditor.ViewModels.Shared;
using ReactiveUI;

namespace NGameEditor.Functionality.Controllers;



public class ProjectController(
	IProjectOpener projectOpener,
	IProjectCreator projectCreator,
	ILogger<ProjectController> logger
) : IProjectController
{
	public ReactiveCommand<OpenExistingProjectArgs, Unit> OpenExistingProject() =>
		ReactiveCommand.CreateFromTask<OpenExistingProjectArgs>(OnOpenExistingProject);


	public ReactiveCommand<CreateProjectDialogArgs, Unit> CreateProject() =>
		ReactiveCommand.CreateFromTask<CreateProjectDialogArgs>(OnCreateProject);


	private async Task<Unit> OnOpenExistingProject(OpenExistingProjectArgs openExistingProjectArgs)
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


	private async Task OnCreateProject(CreateProjectDialogArgs createProjectDialogArgs)
	{
		var folderPicker = createProjectDialogArgs.FolderPicker;

		var filePaths = await folderPicker.AskUserToPickFolder(
			new IFolderPicker.OpenOptions
			{
				Title = "Select folder",
				AllowMultiple = false
			}
		);

		var projectParentPath = filePaths.First();
		var projectParentFolder = new AbsolutePath(projectParentPath);
		// TODO dialog for project name and folder
		var projectName = "UI-Created";


		await projectCreator
			.CreateProject(projectParentFolder, projectName)
			.ThenAsync(projectOpener.OpenProject)
			.FinallyAsync(() => { }, logger.Log);
	}
}
