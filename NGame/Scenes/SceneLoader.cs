using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGame.Ecs;

namespace NGame.Scenes;

public interface ISceneLoader
{
	void LoadStartupScene();
}



public class SceneLoader : ISceneLoader
{
	private readonly SceneConfiguration _sceneConfiguration;
	private readonly IAssetSerializer<Scene> _assetSerializer;
	private readonly IHostEnvironment _hostEnvironment;
	private readonly ILogger<SceneLoader> _logger;
	private readonly IEntityTracker _entityTracker;


	public SceneLoader(
		SceneConfiguration sceneConfiguration,
		IAssetSerializer<Scene> assetSerializer,
		IHostEnvironment hostEnvironment,
		ILogger<SceneLoader> logger,
		IEntityTracker entityTracker
	)
	{
		_sceneConfiguration = sceneConfiguration;
		_assetSerializer = assetSerializer;
		_hostEnvironment = hostEnvironment;
		_logger = logger;
		_entityTracker = entityTracker;
	}


	public void LoadStartupScene()
	{
		var startupScenePath =
			_hostEnvironment
				.ContentRootFileProvider
				.GetFileInfo(_sceneConfiguration.StartupScene)
				.PhysicalPath!;

		if (!File.Exists(startupScenePath))
		{
			_logger.LogError("Can not find startup scene at {0}", startupScenePath);
			throw new InvalidOperationException($"Can not find startup scene at {startupScenePath}");
		}

		var scene = _assetSerializer.Deserialize(startupScenePath);

		foreach (var entity in scene.Entities)
		{
			_entityTracker.AddEntity(entity);
		}
		
		_logger.LogInformation("Got scene with {0} entities", scene.Entities.Count);
	}
}
