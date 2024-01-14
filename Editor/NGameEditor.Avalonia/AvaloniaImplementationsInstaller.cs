using System;
using Avalonia;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Avalonia.AboutWindows;
using NGameEditor.Avalonia.LauncherWindows;
using NGameEditor.Avalonia.ProjectWindows;
using NGameEditor.Avalonia.ProjectWindows.Logs;
using NGameEditor.Avalonia.ProjectWindows.ObjectSelectors;
using NGameEditor.Avalonia.Shared;
using NGameEditor.Functionality.Logging;
using NGameEditor.Functionality.Shared;
using NGameEditor.Functionality.Windows;
using NGameEditor.Functionality.Windows.LauncherWindow;
using NGameEditor.Functionality.Windows.ProjectWindow;
using NGameEditor.Functionality.Windows.ProjectWindow.Inspector;
using NGameEditor.ViewModels.AboutWindows;
using NGameEditor.ViewModels.LauncherWindows;
using NGameEditor.ViewModels.ProjectWindows;
using NGameEditor.ViewModels.ProjectWindows.Logs;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;
using LogWindow = NGameEditor.Avalonia.ProjectWindows.Logs.LogWindow;

namespace NGameEditor.Avalonia;



public static class AvaloniaImplementationsInstaller
{
	public static void AddAvaloniaImplementations(this IHostApplicationBuilder builder)
	{
		builder.Services.AddTransient<IFilePickerOpener, FilePickerOpener>();


		builder.Services.AddWindow<LauncherWindow, LauncherWindowViewModel>();
		builder.Services.AddSingleton<ILauncherWindow, LauncherWindowContainer>();

		builder.Services.AddWindow<ProjectWindow, ProjectWindowViewModel>();
		builder.Services.AddSingleton<IProjectWindow, ProjectWindowContainer>();

		builder.Services.AddWindow<AboutWindow, AboutWindowViewModel>();
		builder.Services.AddSingleton<IAboutWindow, AboutWindowContainer>();

		builder.Services.AddWindow<LogWindow, LogWindowModel>();
		builder.Services.AddSingleton<ILogWindow, LogWindowContainer>();

		builder.Services.AddWindow<ObjectSelectorWindow, ObjectSelectorViewModel>();
		builder.Services.AddSingleton<IObjectSelectorWindow, ObjectSelectorWindowContainer>();


		builder.Services.AddTransient<IDispatcher>(_ => Dispatcher.UIThread);
		builder.Services.AddTransient<IUiThreadDispatcher, UiThreadDispatcher>();
	}


	private static void AddWindow<TWindow, TViewModel>(this IServiceCollection serviceCollection)
		where TWindow : StyledElement, IView<TViewModel>, new()
		where TViewModel : class
	{
		serviceCollection.AddTransient(services =>
			new TWindow { DataContext = services.GetRequiredService<TViewModel>() }
		);

		serviceCollection.AddTransient<Func<TWindow>>(services =>
			services.GetRequiredService<TWindow>);
	}
}
