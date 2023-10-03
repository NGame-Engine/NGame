using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGame.Application;

namespace NGame.Scenes;



public static class SceneInstaller
{
	public static INGameApplicationBuilder AddSceneSupport(this INGameApplicationBuilder builder)
	{
		var jwtConfiguration = new SceneConfiguration();
		builder.Configuration.GetSection(SceneConfiguration.JsonElementName).Bind(jwtConfiguration);
		builder.Services.AddSingleton(jwtConfiguration);

		builder.Services.AddSingleton<IRootSceneAccessor, RootSceneAccessor>();
		builder.Services.AddSingleton<Scene>();

		return builder;
	}


	public static NGameApplication LoadStartupScene(this NGameApplication app)
	{
		//var sceneLoader = gameRunner.Services.GetRequiredService<ISceneLoader>();
		//sceneLoader.LoadStartupScene();

		return app;
	}
}
