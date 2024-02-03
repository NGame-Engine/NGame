using Microsoft.Extensions.Hosting;
using NGame.Implementations.Assets;
using NGame.Implementations.Ecs;
using NGame.Implementations.Ecs.SceneAssets;
using NGame.Implementations.Parallelism;
using NGame.Implementations.UpdateLoop;
using NGame.Setup;

namespace NGame.Implementations.Setup;



public static class NGameCoreInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddNGameCore(this IHostApplicationBuilder builder)
	{
		builder.AddNGameCommon();
		builder.AddUpdateLoop();
		builder.AddAssets();
		builder.AddParallelism();
		builder.AddEcs();
		builder.AddSceneAssets();

		return builder;
	}
}
