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
	public void OnBeforeApplicationStart()
	{
		var startSceneId = validSceneAssetsConfiguration.StartSceneId;
		var scene = sceneLoader.Load(startSceneId);
		rootSceneAccessor.SetRootScene(scene);
	}
}
