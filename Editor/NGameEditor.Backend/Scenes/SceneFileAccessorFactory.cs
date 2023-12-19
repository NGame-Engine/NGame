using NGame.Assets;
using NGameEditor.Backend.Projects;

namespace NGameEditor.Backend.Scenes;



internal class SceneFileAccessorFactory
{
	private readonly ProjectDefinition _projectDefinition;
	private readonly ISceneFileIdReader _sceneFileIdReader;


	public SceneFileAccessorFactory(ProjectDefinition projectDefinition, ISceneFileIdReader sceneFileIdReader)
	{
		_projectDefinition = projectDefinition;
		_sceneFileIdReader = sceneFileIdReader;
	}


	public ISceneFileWatcher Create()
	{
		var gameProjectFile = _projectDefinition.GameProjectFile;
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

		var sceneFileAccessor = new SceneFileWatcher(
			fileSystemWatcher,
			_sceneFileIdReader,
			currentFiles
		);


		fileSystemWatcher.Changed += sceneFileAccessor.OnChanged;
		fileSystemWatcher.Created += sceneFileAccessor.OnCreated;
		fileSystemWatcher.Deleted += sceneFileAccessor.OnDeleted;
		fileSystemWatcher.Renamed += sceneFileAccessor.OnRenamed;
		fileSystemWatcher.Error += sceneFileAccessor.OnError;

		return sceneFileAccessor;
	}
}
