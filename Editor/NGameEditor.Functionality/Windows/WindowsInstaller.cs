using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGameEditor.Functionality.Windows;



public static class WindowsInstaller
{
	public static void AddWindows(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<ILauncherWindowOpener, LauncherWindowOpener>();

		builder.Services.AddTransient<IHierarchyViewModelFactory, HierarchyViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IHierarchyViewModelFactory>().Create()
		);

		builder.Services.AddTransient<IInspectorEntityViewModelFactory, InspectorEntityViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IInspectorEntityViewModelFactory>().Create()
		);
		
		builder.Services.AddTransient<IMenuViewModelFactory, MenuViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IMenuViewModelFactory>().Create()
		);
		
		builder.Services.AddTransient<IProjectWindowViewModelFactory,ProjectWindowViewModelFactory>();
		builder.Services.AddSingleton(services =>
			services.GetRequiredService<IProjectWindowViewModelFactory>().Create()
		);
	}
}
