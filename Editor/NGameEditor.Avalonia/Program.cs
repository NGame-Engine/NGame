using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGameEditor.Functionality;
using NGameEditor.Functionality.Windows;
using NGameEditor.ViewModels;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace NGameEditor.Avalonia;



class Program
{
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
	{
		using var serviceProvider = SetUpDependencyInjection();

		var appBuilder = BuildAvaloniaApp();
		appBuilder.StartWithClassicDesktopLifetime(args);
	}


	// Avalonia configuration, don't remove; also used by visual designer.
	private static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.WithInterFont()
			.LogToTrace();


	private static ServiceProvider SetUpDependencyInjection()
	{
		var builder = Host.CreateApplicationBuilder();
		builder.Services.UseMicrosoftDependencyResolver();

		// These .InitializeX() methods will add ReactiveUI platform 
		// registrations to your container. They MUST be present if
		// you *override* the default Locator.
		Locator.CurrentMutable.InitializeSplat();
		Locator.CurrentMutable.InitializeReactiveUI();

		builder.Services.AddSingleton<IActivationForViewFetcher>(new AvaloniaActivationForViewFetcher());
		builder.Services.AddSingleton<IPropertyBindingHook>(new AutoDataTemplateBindingHook());
		RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;


		builder.AddViewModels();
		builder.AddFunctionality();
		builder.AddAvaloniaImplementations();

		var serviceProvider = builder.Services.BuildServiceProvider();
		serviceProvider.UseMicrosoftDependencyResolver();

		return serviceProvider;
	}
}
