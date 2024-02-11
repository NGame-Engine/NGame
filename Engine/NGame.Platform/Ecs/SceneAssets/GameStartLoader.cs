using NGame.Ecs;
using NGame.Platform.Setup;

namespace NGame.Platform.Ecs.SceneAssets;



public class GameStartLoader(
	ValidSceneAssetsConfiguration validSceneAssetsConfiguration,
	ISceneLoader sceneLoader,
	IRootSceneAccessor rootSceneAccessor
)
	: IBeforeApplicationStartListener
{
	public void OnBeforeApplicationStart() =>
		sceneLoader
			.Load(validSceneAssetsConfiguration.StartSceneId)
			.Completed += rootSceneAccessor.SetRootScene;
}
