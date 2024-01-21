using NGame.Assets;
using NGameEditor.Backend.Files;

namespace NGameEditor.Backend.Scenes;



internal class SceneFileAccessorFactory(
	ISceneFileIdReader sceneFileIdReader,
	IProjectFileWatcher projectFileWatcher
)
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
