using NGame.Assets.Common.Ecs;
using NGameEditor.Backend.Configurations;
using NGameEditor.Backend.Files;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public interface IStartSceneLoader
{
	Result<BackendScene> GetStartScene();
}



internal class StartSceneLoader(
	IGameConfigurationService gameConfigurationService,
	IAssetFileWatcher assetFileWatcher,
	ISceneFileReader sceneFileReader
)
	: IStartSceneLoader
{
	public Result<BackendScene> GetStartScene() =>
		gameConfigurationService
			.GetSection<SceneAssetsConfiguration>(SceneAssetsConfiguration.JsonElementName)
			.Then(x => assetFileWatcher.GetById(x.StartScene))
			.Then(x => x.FilePath.ToAbsoluteFilePath())
			.Then(sceneFileReader.ReadSceneFile);
}
