using NGameEditor.Backend.Projects;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.Files;



internal class ProjectFileInitializer(
	ProjectDefinition projectDefinition,
	IFrontendApi frontendApi,
	ProjectFileStatus projectFileStatus,
	IAssetFileWatcher assetFileWatcher
) : IBackendStartListener
{
	public int Priority => 100;


	public void OnBackendStarted()
	{
		projectFileStatus.DirectoriesChanged += frontendApi.UpdateFiles;


		var solutionFilePath = projectDefinition.SolutionFilePath;
		var solutionDirectory = solutionFilePath.ParentDirectory!;

		var directoryDescriptions =
			Directory
				.GetDirectories(solutionDirectory.PathExport)
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
				.Select(CreateFileDescription)
				.ToList();

		return
			new DirectoryDescription(
				directoryName,
				subDirectories,
				files
			);
	}


	private FileDescription CreateFileDescription(string filePath)
	{
		var assetTypeDefinition =
			assetFileWatcher
				.GetAssetDescriptions()
				.Where(x => x.FilePath.Path == filePath)
				.Select(x => x.AssetTypeDefinition)
				.FirstOrDefault();

		return new FileDescription(filePath, assetTypeDefinition);
	}
}
