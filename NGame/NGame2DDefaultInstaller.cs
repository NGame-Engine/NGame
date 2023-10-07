using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Application;
using NGame.Assets;
using NGame.Ecs;
using NGame.GameLoop;
using NGame.Services;
using NGame.Services.Parallelism;
using NGame.Services.Scenes;
using NGame.Services.Transforms;
using NGame.Setup;

namespace NGame;



public static class NGame2DDefaultInstaller
{
	public static void AddNGame2DDefault(this INGameApplicationBuilder builder)
	{
		if (builder.Environment.IsDevelopment())
		{
			builder.Logging.AddConsole();
		}

		if (builder.Environment.Platform.IsMobile())
		{
			builder.Services.AddSingleton<ITaskScheduler, SequentialTaskScheduler>();
		}
		else
		{
			builder.Services.AddSingleton<ITaskScheduler, ParallelTaskScheduler>();
		}

		builder.AddSystemsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		builder.AddComponentsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);

		builder.Services.AddSingleton<IUpdateScheduler, UpdateScheduler>();
		builder.Services.AddSingleton<IUpdatableCollection, UpdatableCollection>();
		builder.Services.AddSingleton<IDrawableCollection, DrawableCollection>();

		builder.Services.AddSingleton<ISystemCollection, SystemCollection>();
		builder.Services.AddSingleton<IComponentTypeRegistry, ComponentTypeRegistry>();

		builder.Services.AddSingleton<IEntityRegistry, EntityRegistry>();

		builder.Services.AddSingleton<ActionCache>();
		builder.Services.AddSingleton<IActionCache>(services => services.GetRequiredService<ActionCache>());

		builder.Services.AddTransient<EventConnector>();

		builder.Services.AddSingleton<ITagRetriever, TagRetriever>();

		builder.Services.AddSingleton<TransformProcessor>();
		builder.Services.AddSingleton<IMatrixUpdater, MatrixUpdater>();

		var jwtConfiguration = new SceneConfiguration();
		builder.Configuration.GetSection(SceneConfiguration.JsonElementName).Bind(jwtConfiguration);
		builder.Services.AddSingleton(jwtConfiguration);

		builder.Services.AddSingleton<IRootSceneAccessor, RootSceneAccessor>();
		builder.Services.AddSingleton<Scene>();

		builder.Services.AddSingleton(builder.Environment);
		builder.Services.AddSingleton<IApplicationEvents, ApplicationEvents>();
		builder.Services.AddSingleton<IGameRunner, GameRunner>();
	}


	public static void UseNGame2DDefault(this INGameApplication app)
	{
		app.UseUpdatable<ActionCache>();

		var eventConnector = app.Services.GetRequiredService<EventConnector>();
		eventConnector.ConnectEvents();

		app.UseDrawable<TransformProcessor>();

		app.RegisterSystemsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		app.RegisterComponentsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		
		var applicationEvents = app.Services.GetRequiredService<IApplicationEvents>();
		var gameRunner = app.Services.GetRequiredService<IGameRunner>();
		applicationEvents.Closing += (_, _) => gameRunner.Stop();

	}
}
