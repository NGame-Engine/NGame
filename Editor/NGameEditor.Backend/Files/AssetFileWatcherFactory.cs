using NGame.Assets;
using NGameEditor.Backend.Scenes;

namespace NGameEditor.Backend.Files;



internal interface IAssetFileWatcherFactory
{
	IAssetFileWatcher Create();
}



internal class AssetFileWatcherFactory(
	ISceneFileIdReader sceneFileIdReader,
	IProjectFileWatcher projectFileWatcher
) : IAssetFileWatcherFactory
{
	public IAssetFileWatcher Create()
	{
		var currentFiles =
			projectFileWatcher
				.GetAllFiles()
				.Where(x => x.Path.EndsWith(AssetConventions.SceneFileEnding))
				.ToHashSet();

		var sceneFileWatcher = new AssetFileWatcher(
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
