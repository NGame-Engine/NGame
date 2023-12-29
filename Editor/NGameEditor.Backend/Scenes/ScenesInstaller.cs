using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Backend.Scenes.Properties;
using NGameEditor.Backend.Scenes.SceneStates;

namespace NGameEditor.Backend.Scenes;



public static class ScenesInstaller
{
	public static void AddScenes(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ISceneStateFactory, SceneStateFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<ISceneStateFactory>().Create()
		);

		builder.Services.AddTransient<SceneFileAccessorFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<SceneFileAccessorFactory>().Create()
		);


		builder.Services.AddTransient<ILastOpenedSceneLoader, LastOpenedSceneLoader>();
		builder.Services.AddTransient<IStartSceneLoader, StartSceneLoader>();
		builder.Services.AddTransient<ISceneFileIdReader, SceneFileIdReader>();
		builder.Services.AddTransient<ISceneFileReader, SceneFileReader>();
		builder.Services.AddTransient<ISceneDescriptionMapper, SceneDescriptionMapper>();
		builder.Services.AddTransient<ISceneSaver, SceneSaver>();

		builder.Services.AddTransient<IComponentMapper, BoolComponentMapper>();
		builder.Services.AddTransient<IComponentMapper, IntComponentMapper>();
		builder.Services.AddTransient<IComponentMapper, FloatComponentMapper>();
		builder.Services.AddTransient<IComponentMapper, StringComponentMapper>();
	}
}
