using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Shared;
using NGameEditor.Results;
using NGameEditor.ViewModels.LauncherWindows;
using NGameEditor.ViewModels.Shared;

namespace NGameEditor.Functionality.Projects;



public interface IProjectCreator
{
	Task CreateProject(CreateProjectDialogArgs createProjectDialogArgs);
}



public class ProjectCreator(
	ICommandRunner commandRunner,
	IProjectOpener projectOpener,
	ILogger<ProjectCreator> logger
) : IProjectCreator
{
	public async Task CreateProject(CreateProjectDialogArgs createProjectDialogArgs)
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


		await
			CreateProject(projectParentFolder, projectName)
				.ThenAsync(projectOpener.OpenProject)
				.FinallyAsync(() => { }, logger.Log);
	}


	private Result<ProjectId> CreateProject(AbsolutePath directory, string name)
	{
		var listTemplatesOutput = commandRunner.Run(
			"dotnet",
			"new list ngame"
		);

		if (listTemplatesOutput.Contains("NGame Project") == false)
		{
			return Result.Error("NGame Project template not installed");
		}


		var createOutput = commandRunner.Run(
			"dotnet",
			$"new ngame -n {name}",
			directory.Path
		);

		var successString = "The template \"NGame Project\" was created successfully.";
		if (createOutput.Contains(successString) == false)
		{
			return Result.Error($"Unable to create project: {createOutput}");
		}

		var solutionFolder = directory.CombineWith(name);
		var solutionFile = solutionFolder.CombineWith($"{name}.sln");
		var projectId = new ProjectId(solutionFile);


		return Result.Success(projectId);
	}
}
