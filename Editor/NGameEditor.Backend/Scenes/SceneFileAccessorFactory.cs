using NGame.Assets;
using NGameEditor.Backend.Projects;

namespace NGameEditor.Backend.Scenes;



internal class SceneFileAccessorFactory(
	ProjectDefinition projectDefinition,
	ISceneFileIdReader sceneFileIdReader
)
{
	public ISceneFileWatcher Create()
	{
		var gameProjectFile = projectDefinition.GameProjectFile;
		var gameFolder = gameProjectFile.GetParentDirectory()!;

		var fileSystemWatcher = new FileSystemWatcher(gameFolder.Path);

		fileSystemWatcher.NotifyFilter =
			NotifyFilters.Attributes
			| NotifyFilters.CreationTime
			| NotifyFilters.DirectoryName
			| NotifyFilters.FileName
			| NotifyFilters.LastAccess
			| NotifyFilters.LastWrite
			| NotifyFilters.Security
			| NotifyFilters.Size;


		var filePattern = $"*{AssetConventions.SceneFileEnding}";
		fileSystemWatcher.Filter = filePattern;
		fileSystemWatcher.IncludeSubdirectories = true;
		fileSystemWatcher.EnableRaisingEvents = true;


		var currentFiles =
			Directory.GetFiles(
					gameFolder.Path,
					filePattern,
					SearchOption.AllDirectories
				)
				.ToHashSet();

		var sceneFileWatcher = new SceneFileWatcher(
			fileSystemWatcher,
			sceneFileIdReader,
			currentFiles
		);


		fileSystemWatcher.Changed += sceneFileWatcher.OnChanged;
		fileSystemWatcher.Created += sceneFileWatcher.OnCreated;
		fileSystemWatcher.Deleted += sceneFileWatcher.OnDeleted;
		fileSystemWatcher.Renamed += sceneFileWatcher.OnRenamed;
		fileSystemWatcher.Error += sceneFileWatcher.OnError;

		return sceneFileWatcher;
	}
}
