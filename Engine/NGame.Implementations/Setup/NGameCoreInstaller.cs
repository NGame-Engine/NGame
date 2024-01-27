using Microsoft.Extensions.Hosting;
using NGame.Core.Assets;
using NGame.Core.Ecs;
using NGame.Core.Parallelism;
using NGame.Core.SceneAssets;
using NGame.Core.UpdateLoop;
using NGame.Setup;

namespace NGame.Core.Setup;



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
