using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Assets;
using NGame.Assets.Common.Ecs;

namespace NGame.Assets.Common.Setup;



public static class NGameAssetsCommonInstaller
{
	public static void AddNGameAssetsCommon(this IHostApplicationBuilder builder)
	{
		builder.AddNGameCoreAssets();
		builder.Services.AddNGameCoreSceneAssets();
	}
}
