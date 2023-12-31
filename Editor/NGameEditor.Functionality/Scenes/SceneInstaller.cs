using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Functionality.Scenes;



public static class SceneInstaller
{
	public static void AddScenes(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ISceneSaver, SceneSaver>();
		builder.Services.AddTransient<IEntityNodeViewModelMapper, EntityNodeViewModelMapper>();
		builder.Services.AddTransient<IComponentStateMapper, ComponentStateMapper>();
		builder.Services.AddTransient<IInspectorComponentViewModelMapper, InspectorComponentViewModelMapper>();
		builder.Services.AddTransient<ISceneUpdater, SceneUpdater>();
		builder.Services.AddTransient<IEntityCreator, EntityCreator>();
	}
}
