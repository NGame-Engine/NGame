using NGame.Assets.Common.Assets;

namespace NGameEditor.Backend.Files;



internal interface IAssetFileWatcherFactory
{
	IAssetFileWatcher Create();
}



internal class AssetFileWatcherFactory(
	IProjectFileWatcher projectFileWatcher,
	IAssetDescriptionReader assetDescriptionReader
) : IAssetFileWatcherFactory
{
	public IAssetFileWatcher Create()
	{
		var currentFiles =
			projectFileWatcher
				.GetAllFiles()
				.Where(x => x.PathExport.EndsWith(AssetConventions.AssetFileEnding))
				.Select(assetDescriptionReader.ReadAsset);

		var sceneFileWatcher = new AssetFileWatcher(
			currentFiles,
			assetDescriptionReader
		);

		projectFileWatcher.FileChanged += sceneFileWatcher.OnChanged;
		projectFileWatcher.FileCreated += sceneFileWatcher.OnCreated;
		projectFileWatcher.FileDeleted += sceneFileWatcher.OnDeleted;
		projectFileWatcher.FileRenamed += sceneFileWatcher.OnRenamed;

		return sceneFileWatcher;
	}
}
