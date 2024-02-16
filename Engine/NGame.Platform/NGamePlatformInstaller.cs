using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Setup;
using NGame.Platform.Assets;
using NGame.Platform.Ecs;
using NGame.Platform.Ecs.Implementations;
using NGame.Platform.Ecs.SceneAssets;
using NGame.Platform.Parallelism;
using NGame.Platform.UpdateLoop;

namespace NGame.Platform;



public static class NGamePlatformInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddNGamePlatform(this IHostApplicationBuilder builder)
	{
		builder.AddNGameAssetsCommon();
		builder.Services.AddNGameCoreEcs();
		builder.AddPlatformUpdateLoop();
		builder.AddPlatformAssets();
		builder.AddParallelism();
		builder.AddEcs();
		builder.AddSceneAssets();

		return builder;
	}
}
