using Microsoft.Extensions.DependencyInjection;

namespace NGame.Assets.Common.Ecs;



public static class NGameCoreSceneAssetsInstaller
{
	public static void AddNGameCoreSceneAssets(this IServiceCollection services)
	{
		services.AddTransient<ISceneSerializerOptionsFactory, SceneSerializerOptionsFactory>();
	}
}
