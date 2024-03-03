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
		startSceneId = Guid.Parse("0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC"); // TODO Get appsettings.json on android
		var scene = sceneLoader.Load(startSceneId);
		rootSceneAccessor.SetRootScene(scene);
	}
}
