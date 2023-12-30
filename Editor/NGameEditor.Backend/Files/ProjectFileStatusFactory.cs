using NGameEditor.Backend.Projects;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.Files;



public interface IProjectFileStatusFactory
{
	ProjectFileStatus Create();
}



internal class ProjectFileStatusFactory(
	ProjectDefinition projectDefinition,
	IFrontendApi frontendApi
) : IProjectFileStatusFactory
{
	public ProjectFileStatus Create()
	{
		var projectFileStatus = new ProjectFileStatus();
		projectFileStatus.DirectoriesChanged += frontendApi.UpdateFiles;


		var solutionFilePath = projectDefinition.SolutionFilePath;
		var solutionDirectory = solutionFilePath.GetParentDirectory()!;

		var directoryDescriptions =
			Directory
				.GetDirectories(solutionDirectory.Path)
				.Select(GetDirectoryDescriptionsRecursive)
				.ToList();

		projectFileStatus.SetDirectories(directoryDescriptions);


		return projectFileStatus;
	}


	private DirectoryDescription GetDirectoryDescriptionsRecursive(
		string directoryPath
	)
	{
		var directoryName = new DirectoryInfo(directoryPath).Name;

		var subDirectories =
			Directory
				.GetDirectories(directoryPath)
				.Select(GetDirectoryDescriptionsRecursive)
				.ToList();

		var files =
			Directory
				.GetFiles(directoryPath)
				.Select(x => new FileDescription(x))
				.ToList();

		return
			new DirectoryDescription(
				directoryName,
				subDirectories,
				files
			);
	}
}
