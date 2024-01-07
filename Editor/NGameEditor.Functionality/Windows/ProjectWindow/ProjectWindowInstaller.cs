using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Functionality.Scenes.State;
using NGameEditor.ViewModels.ProjectWindows;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;
using NGameEditor.ViewModels.ProjectWindows.Logs;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;

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


		builder.Services.AddTransient<IInspectorViewModelFactory, InspectorViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IInspectorViewModelFactory>().Create()
		);


		builder.Services.AddTransient<IMenuViewModelFactory, MenuViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IMenuViewModelFactory>().Create()
		);

		builder.Services.AddTransient<IProjectWindowViewModelFactory, ProjectWindowViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectWindowViewModelFactory>().Create()
		);


		builder.Services.AddSingleton<FileBrowserViewModel>();
		builder.Services.AddSingleton<DirectoryOverviewViewModel>();

		builder.Services.AddTransient<IDirectoryContentViewModelFactory, DirectoryContentViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IDirectoryContentViewModelFactory>().Create()
		);


		builder.Services.AddSingleton<ObjectSelectionState>();
		builder.Services.AddTransient<IObjectViewModelMapper, ObjectViewModelMapper>();
		builder.Services.AddTransient<IObjectSelectorViewModelFactory, ObjectSelectorViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IObjectSelectorViewModelFactory>().Create()
		);

		builder.Services.AddTransient<ISelectedObjectViewModelFactory, SelectedObjectViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<ISelectedObjectViewModelFactory>().Create()
		);


		builder.Services.AddTransient<IAddComponentMenuEntryFactory, AddComponentMenuEntryFactory>();
	}
}
