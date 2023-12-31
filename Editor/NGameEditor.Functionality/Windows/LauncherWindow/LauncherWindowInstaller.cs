using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.ViewModels.LauncherWindows;
using NGameEditor.ViewModels.LauncherWindows.HistoryViews;
using NGameEditor.ViewModels.LauncherWindows.Logs;

namespace NGameEditor.Functionality.Windows.LauncherWindow;



public static class LauncherWindowInstaller
{
	public static void AddLauncherWindow(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<LauncherWindowViewModel>();

		builder.Services.AddTransient<ILauncherWindowOpener, LauncherWindowOpener>();
		builder.Services.AddSingleton<ProjectHistoryViewModel>();
		builder.Services.AddSingleton<LauncherLogViewModel>();

		builder.Services.AddTransient<IProjectOperationsViewModelFactory, ProjectOperationsViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectOperationsViewModelFactory>().Create()
		);

		builder.Services.AddTransient<IProjectHistoryViewModelFactory, ProjectHistoryViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectHistoryViewModelFactory>().Create()
		);
	}
}
