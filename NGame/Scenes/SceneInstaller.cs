using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGame.Assets;
using NGame.Setup;

namespace NGame.Scenes;

public static class SceneInstaller
{
	public static void AddSceneSupport(this INGameApplicationBuilder gameBuilder)
	{
		var jwtConfiguration = new SceneConfiguration();
		gameBuilder.Configuration.GetSection(SceneConfiguration.JsonElementName).Bind(jwtConfiguration);
		gameBuilder.Services.AddSingleton(jwtConfiguration);

		gameBuilder.Services.AddSingleton<IAssetSerializer<Scene>, SceneSerializer>();
		gameBuilder.Services.AddTransient<ISceneLoader, SceneLoader>();
	}


	public static void LoadStartupScene(this NGameApplication app)
	{
		//var sceneLoader = gameRunner.Services.GetRequiredService<ISceneLoader>();
		//sceneLoader.LoadStartupScene();
	}
}
