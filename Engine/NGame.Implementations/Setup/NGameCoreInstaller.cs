using NGame.Core.Assets;
using NGame.Core.Ecs;
using NGame.Core.Ecs.SceneAssets;
using NGame.Core.Parallelism;
using NGame.Core.UpdateLoop;
using NGame.Setup;

namespace NGame.Core.Setup;



public static class NGameCoreInstaller
{
	public static INGameBuilder AddNGameCore(this INGameBuilder builder)
	{
		builder.Services.AddNGameCommon();
		builder.AddUpdateLoop();
		builder.AddAssets();
		builder.AddParallelism();
		builder.AddEcs();
		builder.AddSceneAssets();

		return builder;
	}
}
