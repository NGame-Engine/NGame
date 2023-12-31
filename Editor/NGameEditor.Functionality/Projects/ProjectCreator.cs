using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Shared;
using NGameEditor.Functionality.Windows.ProjectWindow;
using NGameEditor.Results;

namespace NGameEditor.Functionality.Projects;



public interface IProjectCreator
{
	Task CreateProject();
}



public class ProjectCreator(
	ICommandRunner commandRunner,
	IProjectOpener projectOpener,
	ILogger<ProjectCreator> logger,
	IProjectWindow projectWindow
) : IProjectCreator
{
	public async Task CreateProject()
	{
		var filePaths = await projectWindow.AskUserToPickFolder(
			new OpenFolderOptions
			{
				Title = "Select folder",
				AllowMultiple = false
			}
		);

		var projectParentPath = filePaths.FirstOrDefault();
		if (projectParentPath == null) return;

		var projectParentFolder = new AbsolutePath(projectParentPath);
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
