using Microsoft.Extensions.Hosting;
using NGame.Assets.Implementations;
using NGame.Ecs.Implementations;
using NGame.SceneAssets.Implementations;

namespace NGame.Setup;



public static class NGameCommonInstaller
{
	public static void AddNGameCommon(this IHostApplicationBuilder builder)
	{
		builder.Services.AddNGameCoreEcs();
		builder.Services.AddNGameCoreAssets();
		builder.Services.AddNGameCoreSceneAssets();
	}
}
