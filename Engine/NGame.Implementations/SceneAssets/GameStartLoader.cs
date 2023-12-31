using NGame.Ecs;
using NGame.SceneAssets;
using NGame.Setup;

namespace NGame.Core.SceneAssets;



public class GameStartLoader : IBeforeApplicationStartListener
{
	private readonly ISceneAssetsConfigurationValidator _sceneAssetsConfigurationValidator;
	private readonly ISceneLoader _sceneLoader;
	private readonly IRootSceneAccessor _rootSceneAccessor;


	public GameStartLoader(
		ISceneAssetsConfigurationValidator sceneAssetsConfigurationValidator,
		ISceneLoader sceneLoader,
		IRootSceneAccessor rootSceneAccessor
	)
	{
		_sceneAssetsConfigurationValidator = sceneAssetsConfigurationValidator;
		_sceneLoader = sceneLoader;
		_rootSceneAccessor = rootSceneAccessor;
	}


	public void OnBeforeApplicationStart()
	{
		var configuration = _sceneAssetsConfigurationValidator.Validate();
		var startSceneId = configuration.StartSceneId;

		_sceneLoader
			.Load(startSceneId)
			.Completed += scene => _rootSceneAccessor.SetRootScene(scene);
	}
}
