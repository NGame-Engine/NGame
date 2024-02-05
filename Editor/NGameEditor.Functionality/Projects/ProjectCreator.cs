using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.Projects;
using NGameEditor.Functionality.Files;
using NGameEditor.Functionality.Shared;
using NGameEditor.Functionality.Windows.LauncherWindow;
using NGameEditor.Results;
using Singulink.IO;

namespace NGameEditor.Functionality.Projects;



public interface IProjectCreator
{
	Task CreateProject();
}



public class ProjectCreator(
	ICommandRunner commandRunner,
	IProjectOpener projectOpener,
	ILogger<ProjectCreator> logger,
	ILauncherWindow launcherWindow
) : IProjectCreator
{
	public async Task CreateProject()
	{
		var filePaths = await launcherWindow.AskUserToPickFolder(
			new OpenFolderOptions
			{
				Title = "Select folder",
				AllowMultiple = false,
				SuggestedStartLocation = null
			}
		);

		var projectParentPath = filePaths.FirstOrDefault();
		if (projectParentPath == null) return;

		var projectParentFolder = DirectoryPath.ParseAbsolute(projectParentPath);
		const string projectName = "UI-Created";

		await
			CreateProject(projectParentFolder, projectName)
				.ThenAsync(projectOpener.OpenProject)
				.FinallyAsync(() => { }, logger.Log);
	}


	private Result<ProjectId> CreateProject(IAbsoluteDirectoryPath directory, string name)
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
			directory.PathExport
		);

		const string successString = "The template \"NGame Project\" was created successfully.";
		if (createOutput.Contains(successString) == false)
		{
			return Result.Error($"Unable to create project: {createOutput}");
		}

		var solutionFolder = directory.CombineDirectory(name);
		var solutionFile = solutionFolder.CombineFile($"{name}.sln");
		var projectId = new ProjectId(solutionFile.ToIAbsolutePath());


		return Result.Success(projectId);
	}
}
