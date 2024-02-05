using NGameEditor.Backend.Projects;
using Singulink.IO;

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
		var solutionFolder = solutionFilePath.ParentDirectory!;

		var currentFiles =
			Directory
				.EnumerateFiles(
					solutionFolder.PathExport,
					"*.*",
					SearchOption.AllDirectories
				)
				.Where(x =>
					x.Contains("/bin/") == false &&
					x.Contains("/obj/") == false
				)
				.Select(x => FilePath.ParseAbsolute(x))
				.ToHashSet();


		var fileSystemWatcher = new FileSystemWatcher(solutionFolder.PathExport)
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
		fileSystemWatcher.Error += (_, e) => throw e.GetException();

		return projectFileWatcher;
	}
}
