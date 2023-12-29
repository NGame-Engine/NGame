using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Functionality.Windows;



public static class WindowsInstaller
{
	public static void AddWindows(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ILauncherWindowOpener, LauncherWindowOpener>();
	}
}
