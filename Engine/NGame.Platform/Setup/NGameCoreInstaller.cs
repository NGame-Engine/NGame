using Microsoft.Extensions.Hosting;
using NGame.Assets.Common.Setup;
using NGame.Platform.Assets;
using NGame.Platform.Ecs;
using NGame.Platform.Ecs.Implementations;
using NGame.Platform.Ecs.SceneAssets;
using NGame.Platform.Parallelism;
using NGame.Platform.UpdateLoop;

namespace NGame.Platform.Setup;



public static class NGameCoreInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddNGameCore(this IHostApplicationBuilder builder)
	{
		builder.AddNGameCommon();
		builder.Services.AddNGameCoreEcs();
		builder.AddUpdateLoop();
		builder.AddAssets();
		builder.AddParallelism();
		builder.AddEcs();
		builder.AddSceneAssets();

		return builder;
	}
}
