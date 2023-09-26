using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGame.Ecs;
using NGame.Renderers;
using NGame.UpdaterSchedulers;

namespace NGame.Setup;

public static class NGameApplicationBuilderExtensions
{
	public static INGameApplicationBuilder AddNGame(this INGameApplicationBuilder builder)
	{
		builder.Logging.AddConsole();

		builder.Services.AddSingleton<IUpdateScheduler, UpdateScheduler>();
		builder.Services.AddSingleton<IUpdatableCollection, UpdatableCollection>();

		builder.AddComponentSystem();


		return builder;
	}


	public static NGameApplication UseNGame(this NGameApplication app)
	{
		return app;
	}
}
