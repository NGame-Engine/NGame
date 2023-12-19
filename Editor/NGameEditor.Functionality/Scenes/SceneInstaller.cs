using Microsoft.Extensions.DependencyInjection;

namespace NGameEditor.Functionality.Scenes;



public static class SceneInstaller
{
	public static void AddScenes(this IServiceCollection services)
	{
		services.AddTransient<ISceneSaver, SceneSaver>();
		services.AddTransient<IComponentStateMapper, ComponentStateMapper>();
	}
}
