using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.ViewModels.ProjectWindows;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.Logs;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public static class ProjectWindowInstaller
{
	public static void AddProjectWindow(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<SceneState>();
		builder.Services.AddSingleton<SelectedEntitiesState>();


		builder.Services.AddSingleton<ProjectWindowViewModel>();

		builder.Services.AddSingleton<LogWindowModel>();


		builder.Services.AddTransient<IHierarchyViewModelFactory, HierarchyViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IHierarchyViewModelFactory>().Create()
		);


		builder.Services.AddSingleton<InspectorViewModel>();

		builder.Services.AddTransient<IInspectorEntityViewModelFactory, InspectorEntityViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IInspectorEntityViewModelFactory>().Create()
		);


		builder.Services.AddTransient<IMenuViewModelFactory, MenuViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IMenuViewModelFactory>().Create()
		);

		builder.Services.AddTransient<IProjectWindowViewModelFactory, ProjectWindowViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectWindowViewModelFactory>().Create()
		);

		builder.Services.AddTransient<IFileBrowserViewModelFactory, FileBrowserViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IFileBrowserViewModelFactory>().Create()
		);

		builder.Services.AddTransient<IAddComponentMenuEntryFactory, AddComponentMenuEntryFactory>();
	}
}
