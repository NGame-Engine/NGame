using NGame.SceneAssets;
using NGameEditor.Backend.Configurations;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public interface IStartSceneLoader
{
	Result<BackendScene> GetStartScene();
}



public class StartSceneLoader(
	IGameConfigurationService gameConfigurationService,
	ISceneFileWatcher sceneFileWatcher,
	ISceneFileReader sceneFileReader
)
	: IStartSceneLoader
{
	public Result<BackendScene> GetStartScene() =>
		gameConfigurationService
			.GetSection<SceneAssetsConfiguration>(SceneAssetsConfiguration.JsonElementName)
			.Then(x => sceneFileWatcher.GetPathToSceneFile(x.StartScene))
			.Then(sceneFileReader.ReadSceneFile);
}
