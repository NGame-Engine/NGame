using NGame.Assets;
using NGameEditor.Backend.Scenes;

namespace NGameEditor.Backend.Files;



internal interface ISceneFileWatcherFactory
{
	ISceneFileWatcher Create();
}



internal class SceneFileWatcherFactory(
	ISceneFileIdReader sceneFileIdReader,
	IProjectFileWatcher projectFileWatcher
) : ISceneFileWatcherFactory
{
	public ISceneFileWatcher Create()
	{
		var currentFiles =
			projectFileWatcher
				.GetAllFiles()
				.Where(x => x.Path.EndsWith(AssetConventions.SceneFileEnding))
				.ToHashSet();

		var sceneFileWatcher = new SceneFileWatcher(
			sceneFileIdReader,
			currentFiles
		);

		projectFileWatcher.FileChanged += sceneFileWatcher.OnChanged;
		projectFileWatcher.FileCreated += sceneFileWatcher.OnCreated;
		projectFileWatcher.FileDeleted += sceneFileWatcher.OnDeleted;
		projectFileWatcher.FileRenamed += sceneFileWatcher.OnRenamed;

		return sceneFileWatcher;
	}
}
