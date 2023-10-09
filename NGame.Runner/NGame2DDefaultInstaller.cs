using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGame.Ecs;
using NGame.GameLoop;
using NGame.Resources;
using NGame.Services;
using NGame.Services.Scenes;
using NGame.Services.Transforms;
using NGame.Setup;

namespace NGame;



public static class NGame2DDefaultInstaller
{
	public static INGameBuilder AddNGame2DDefault(this INGameBuilder builder)
	{
		var callingAssemblyTitle =
			Assembly
				.GetCallingAssembly()
				.GetCustomAttribute<AssemblyTitleAttribute>()?
				.Title;

		if (callingAssemblyTitle != null)
		{
			builder.Environment.ApplicationName = callingAssemblyTitle;
		}


		if (builder.Environment.IsDevelopment())
		{
			builder.Logging.AddConsole();
		}


		builder.AddSystemsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		builder.AddComponentsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);

		builder.Services.AddSingleton<ITickScheduler, TickScheduler>();
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

		builder.Services.AddSingleton<IResourceRegistry<AudioClip>, ResourceRegistry<AudioClip>>();


		builder.Services.AddSingleton(builder.Environment);


		return builder;
	}


	public static INGame UseNGame2DDefault(this INGame app)
	{
		app.RegisterSystemsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		app.RegisterComponentsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);

		app.UseUpdatable<ActionCache>();
		app.UseDrawable<TransformProcessor>();


		var eventConnector = app.Services.GetRequiredService<EventConnector>();
		eventConnector.ConnectEvents();

		return app;
	}
}
