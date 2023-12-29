using NGameEditor.Backend.Projects;
using NGameEditor.Bridge.Files;

namespace NGameEditor.Backend.Files;



public interface IProjectFileStatusFactory
{
	ProjectFileStatus Create();
}



internal class ProjectFileStatusFactory(
	ProjectDefinition projectDefinition
) : IProjectFileStatusFactory
{
	public ProjectFileStatus Create()
	{
		var solutionFilePath = projectDefinition.SolutionFilePath;
		var solutionDirectory = solutionFilePath.GetParentDirectory()!;

		var directoryDescriptions =
			Directory
				.GetDirectories(solutionDirectory.Path)
				.Select(GetDirectoryDescriptionsRecursive)
				.ToList();


		return new ProjectFileStatus(directoryDescriptions);
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
