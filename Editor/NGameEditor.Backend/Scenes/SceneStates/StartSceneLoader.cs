using NGame.SceneAssets;
using NGameEditor.Backend.Configurations;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public interface IStartSceneLoader
{
	Result<BackendScene> GetStartScene();
}



public class StartSceneLoader : IStartSceneLoader
{
	private readonly IGameConfigurationService _gameConfigurationService;
	private readonly ISceneFileWatcher _sceneFileWatcher;
	private readonly ISceneFileReader _sceneFileReader;


	public StartSceneLoader(
		IGameConfigurationService gameConfigurationService,
		ISceneFileWatcher sceneFileWatcher,
		ISceneFileReader sceneFileReader
	)
	{
		_gameConfigurationService = gameConfigurationService;
		_sceneFileWatcher = sceneFileWatcher;
		_sceneFileReader = sceneFileReader;
	}


	public Result<BackendScene> GetStartScene() =>
		_gameConfigurationService
			.GetSection<SceneAssetsConfiguration>(SceneAssetsConfiguration.JsonElementName)
			.Then(x => _sceneFileWatcher.GetPathToSceneFile(x.StartScene))
			.Then(_sceneFileReader.ReadSceneFile);
}
