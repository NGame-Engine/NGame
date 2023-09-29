using Microsoft.Extensions.DependencyInjection;
using NGame.OsWindows;
using NGame.Setup;

namespace NGamePlugin.Window.Sfml;

public static class WindowSfmlInstaller
{
	public static void AddWindowSfml(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IOsWindow, SfmlWindow>();
	}


	public static void UseWindowSfml(this NGameApplication app)
	{
		var nGameHostedService = app.Services.GetRequiredService<NGameHostedService>();
		var window = app.Services.GetRequiredService<IOsWindow>();
		window.Closed += (_, _) => nGameHostedService.StopAsync();
	}
}
