using Microsoft.Extensions.DependencyInjection;

namespace NGame.Tooling.Ecs;



public static class NGameCoreSceneAssetsInstaller
{
	public static void AddNGameCoreSceneAssets(this IServiceCollection services)
	{
		services.AddTransient<ISceneSerializerOptionsFactory, SceneSerializerOptionsFactory>();
	}
}
