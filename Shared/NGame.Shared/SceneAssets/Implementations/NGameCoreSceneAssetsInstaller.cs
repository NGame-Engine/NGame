using Microsoft.Extensions.DependencyInjection;

namespace NGame.SceneAssets.Implementations;



public static class NGameCoreSceneAssetsInstaller
{
	public static void AddNGameCoreSceneAssets(this IServiceCollection services)
	{
		services.AddTransient<ISceneSerializerOptionsFactory, SceneSerializerOptionsFactory>();
	}
}
