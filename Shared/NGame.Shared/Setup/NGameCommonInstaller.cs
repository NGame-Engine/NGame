using Microsoft.Extensions.DependencyInjection;
using NGame.Assets.Implementations;
using NGame.Ecs.Implementations;
using NGame.SceneAssets.Implementations;

namespace NGame.Setup;



public static class NGameCommonInstaller
{
	public static void AddNGameCommon(this IServiceCollection services)
	{
		services.AddNGameCoreEcs();
		services.AddNGameCoreAssets();
		services.AddNGameCoreSceneAssets();
	}
}
