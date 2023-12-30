using Microsoft.Extensions.DependencyInjection;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Functionality.Scenes;



public static class SceneInstaller
{
	public static void AddScenes(this IServiceCollection services)
	{
		services.AddTransient<ISceneSaver, SceneSaver>();
		services.AddTransient<IEntityNodeViewModelMapper, EntityNodeViewModelMapper>();
		services.AddTransient<IComponentStateMapper, ComponentStateMapper>();
		services.AddTransient<IInspectorComponentViewModelMapper, InspectorComponentViewModelMapper>();
		services.AddTransient<ISceneUpdater, SceneUpdater>();
	}
}
