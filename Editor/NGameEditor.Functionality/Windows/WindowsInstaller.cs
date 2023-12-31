using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Functionality.Windows.LauncherWindow;
using NGameEditor.Functionality.Windows.ProjectWindow;
using NGameEditor.ViewModels.AboutWindows;

namespace NGameEditor.Functionality.Windows;



public static class WindowsInstaller
{
	public static void AddWindows(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<AboutWindowViewModel>();

		builder.AddLauncherWindow();
		builder.AddProjectWindow();
	}
}
