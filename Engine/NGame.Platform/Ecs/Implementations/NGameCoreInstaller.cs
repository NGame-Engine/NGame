using Microsoft.Extensions.DependencyInjection;

namespace NGame.Platform.Ecs.Implementations;



public static class NGameCoreInstaller
{
	public static void AddNGameCoreEcs(this IServiceCollection services)
	{
		services.AddSingleton<IEntityEditor, EntityEditor>();
		services.AddSingleton<ISceneEditor, SceneEditor>();
		services.AddSingleton<IMatrixUpdater, MatrixUpdater>();
	}
}
