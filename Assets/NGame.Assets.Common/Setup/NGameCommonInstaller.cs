using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Assets;
using NGame.Assets.Common.Ecs;

namespace NGame.Assets.Common.Setup;



public static class NGameCommonInstaller
{
	public static void AddNGameCommon(this IHostApplicationBuilder builder)
	{
		builder.Services.AddNGameCoreAssets();
		builder.Services.AddNGameCoreSceneAssets();
	}
}
