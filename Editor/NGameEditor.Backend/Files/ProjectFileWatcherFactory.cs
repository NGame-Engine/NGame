using NGameEditor.Backend.Projects;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Files;



public interface IProjectFileWatcherFactory
{
	IProjectFileWatcher Create();
}



public class ProjectFileWatcherFactory(
	ProjectDefinition projectDefinition
) : IProjectFileWatcherFactory
{
	public IProjectFileWatcher Create()
	{
		var solutionFilePath = projectDefinition.SolutionFilePath;
		var solutionFolder = solutionFilePath.GetParentDirectory()!;

		var currentFiles =
			Directory
				.EnumerateFiles(
					solutionFolder.Path,
					"*.*",
					SearchOption.AllDirectories
					)
				.Where(x =>
					x.Contains("/bin/") == false &&
					x.Contains("/obj/") == false
				)
				.Select(x => new AbsolutePath(x))
				.ToHashSet();


		var fileSystemWatcher = new FileSystemWatcher(solutionFolder.Path)
		{
			IncludeSubdirectories = true,
			EnableRaisingEvents = true,
			NotifyFilter =
				NotifyFilters.Attributes
				| NotifyFilters.CreationTime
				| NotifyFilters.DirectoryName
				| NotifyFilters.FileName
				| NotifyFilters.LastAccess
				| NotifyFilters.LastWrite
				| NotifyFilters.Security
				| NotifyFilters.Size
		};


		var projectFileWatcher = new ProjectFileWatcher(
			currentFiles,
			fileSystemWatcher
			);

		fileSystemWatcher.Changed += projectFileWatcher.OnChanged;
		fileSystemWatcher.Created += projectFileWatcher.OnCreated;
		fileSystemWatcher.Deleted += projectFileWatcher.OnDeleted;
		fileSystemWatcher.Renamed += projectFileWatcher.OnRenamed;
		fileSystemWatcher.Error += projectFileWatcher.OnError;

		return projectFileWatcher;
	}
}
