using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.Functionality.Shared;
using NGameEditor.Results;

namespace NGameEditor.Functionality.Projects;



public interface IProjectCreator
{
	Result<ProjectId> CreateProject(AbsolutePath directory, string name);
}



public class ProjectCreator : IProjectCreator
{
	private readonly ICommandRunner _commandRunner;


	public ProjectCreator(ICommandRunner commandRunner)
	{
		_commandRunner = commandRunner;
	}


	public Result<ProjectId> CreateProject(AbsolutePath directory, string name)
	{
		var listTemplatesOutput = _commandRunner.Run(
			"dotnet",
			"new list ngame"
		);

		if (listTemplatesOutput.Contains("NGame Project") == false)
		{
			return Result.Error("NGame Project template not installed");
		}


		var createOutput = _commandRunner.Run(
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
