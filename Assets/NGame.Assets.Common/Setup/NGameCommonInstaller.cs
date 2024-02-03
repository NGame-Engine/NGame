using Microsoft.Extensions.Hosting;
using NGame.Tooling.Assets;
using NGame.Tooling.Ecs;

namespace NGame.Tooling.Setup;



public static class NGameCommonInstaller
{
	public static void AddNGameCommon(this IHostApplicationBuilder builder)
	{
		builder.Services.AddNGameCoreAssets();
		builder.Services.AddNGameCoreSceneAssets();
	}
}
