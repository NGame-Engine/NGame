using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGame.SceneAssets;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.Scenes.SceneStates;



public class SceneStateInitializer(
	ILastOpenedSceneLoader lastOpenedSceneLoader,
	IStartSceneLoader startSceneLoader,
	ILogger<SceneStateInitializer> logger,
	ISceneState sceneState,
	ISceneDescriptionMapper sceneDescriptionMapper,
	IFrontendApi frontendApi
) : IBackendStartListener
{
	public int Priority => 1000;


	public void OnBackendStarted()
	{
		var backendScene = GetBackendScene();

		sceneState.LoadedSceneChanged += args =>
		{
			var sceneDescription = sceneDescriptionMapper.Map(args.NewBackendScene);
			frontendApi.UpdateLoadedScene(sceneDescription);
		};

		sceneState.SetLoadedScene(backendScene);
	}


	private BackendScene GetBackendScene()
	{
		var lastOpenedSceneResult = lastOpenedSceneLoader.GetLastOpenedScene();
		if (lastOpenedSceneResult.TryGetValue(out var lastOpenedScene))
		{
			return lastOpenedScene;
		}

		logger.LogInformation("{Error}", lastOpenedSceneResult.ErrorValue!.Title);

		var startSceneResult = startSceneLoader.GetStartScene();

		if (startSceneResult.TryGetValue(out var startScene))
		{
			return startScene;
		}

		var newScene =
			new BackendScene(
				null,
				new SceneAsset
				{
					Id = AssetId.Create(Guid.NewGuid())
				}
			);

		return newScene;
	}
}
