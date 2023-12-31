using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.ViewModels.AboutWindows;
using NGameEditor.ViewModels.LauncherWindows;
using NGameEditor.ViewModels.LauncherWindows.HistoryViews;
using NGameEditor.ViewModels.LauncherWindows.Logs;
using NGameEditor.ViewModels.ProjectWindows;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;
using NGameEditor.ViewModels.ProjectWindows.Logs;
using NGameEditor.ViewModels.ProjectWindows.MenuViews;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels;



public static class ViewModelInstaller
{
	public static void AddViewModels(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<AboutWindowViewModel>();


		builder.Services.AddSingleton<LauncherWindowViewModel>();
		builder.Services.AddSingleton<ProjectOperationsViewModel>();
		builder.Services.AddSingleton<ProjectHistoryViewModel>();
		builder.Services.AddSingleton<LauncherLogViewModel>();


		builder.AddProjectWindowViewModels();
	}


	private static void AddProjectWindowViewModels(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<SceneState>();
		builder.Services.AddSingleton<SelectedEntitiesState>();


		builder.Services.AddSingleton<ProjectWindowViewModel>();

		builder.Services.AddSingleton<InspectorViewModel>();


		builder.Services.AddSingleton<LogWindowModel>();
	}
}
