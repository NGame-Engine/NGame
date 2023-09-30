using Microsoft.Extensions.DependencyInjection;
using NGame.Inputs;
using NGame.OsWindows;
using NGame.Setup;
using NGamePlugin.Window.Sfml.Inputs;

namespace NGamePlugin.Window.Sfml;

public static class WindowSfmlInstaller
{
	public static void AddWindowSfml(this INGameApplicationBuilder builder)
	{
		builder.Services.AddTransient<RenderWindowFactory>();
		builder.Services.AddSingleton(
			services =>
				services
					.GetRequiredService<RenderWindowFactory>()
					.Create()
		);

		builder.Services.AddSingleton<SfmlWindow>();
		builder.Services.AddTransient<IOsWindow>(
			services => services.GetRequiredService<SfmlWindow>()
		);

		builder.Services.AddSingleton<RawInputListener>();
		builder.Services.AddTransient<IRawInputListener>(
			services => services.GetRequiredService<RawInputListener>()
		);

		builder.Services.AddTransient<RenderWindowEventConnector>();
	}


	public static void UseWindowSfml(this NGameApplication app)
	{
		var nGameHostedService = app.Services.GetRequiredService<NGameHostedService>();
		var window = app.Services.GetRequiredService<IOsWindow>();
		window.Closed += (_, _) => nGameHostedService.StopAsync();

		var renderWindowEventConnector = app.Services.GetRequiredService<RenderWindowEventConnector>();
		renderWindowEventConnector.ConnectEvents();
	}
}
