using NGameEditor.Backend.Projects;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.Files;



public class ProjectFileInitializer(
	ProjectDefinition projectDefinition,
	IFrontendApi frontendApi,
	ProjectFileStatus projectFileStatus
) : IBackendStartListener
{
	public int Priority => 100;


	public void OnBackendStarted()
	{
		projectFileStatus.DirectoriesChanged += frontendApi.UpdateFiles;


		var solutionFilePath = projectDefinition.SolutionFilePath;
		var solutionDirectory = solutionFilePath.GetParentDirectory()!;

		var directoryDescriptions =
			Directory
				.GetDirectories(solutionDirectory.Path)
				.Select(GetDirectoryDescriptionsRecursive)
				.ToList();

		projectFileStatus.SetDirectories(directoryDescriptions);
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
