using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGame.Ecs;
using NGame.OsWindows;
using NGame.Scenes;
using NGame.UpdateSchedulers;

namespace NGame.Setup;

public static class NGameApplicationBuilderExtensions
{
	public static INGameApplicationBuilder AddNGame(this INGameApplicationBuilder builder)
	{
		builder.Logging.AddConsole();

		builder.Services.AddSingleton<IUpdateScheduler, UpdateScheduler>();
		builder.Services.AddSingleton<IUpdatableCollection, UpdatableCollection>();

		builder.AddComponentSystem();

		builder.AddSceneSupport();


		return builder;
	}


	public static NGameApplication StartNGame(this NGameApplication app)
	{
		var nGameHostedService = app.Services.GetRequiredService<NGameHostedService>();
		var window = app.Services.GetRequiredService<IOsWindow>();
		window.Closed += (_, _) => nGameHostedService.StopAsync();

		app.LoadStartupScene();
		return app;
	}
}
