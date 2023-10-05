using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Application;
using NGame.Components.Transforms;
using NGame.Ecs;
using NGame.NGameSystem.Parallelism;
using NGame.Scenes;
using NGame.UpdateSchedulers;

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

		builder.AddUpdateScheduler();
		builder.AddComponentSystem();
		builder.AddSystemsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		builder.AddComponentsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);

		builder.AddTransforms();

		builder.AddSceneSupport();
	}


	public static void UseNGame2DDefault(this NGameApplication app)
	{
		app.UseComponentSystem();
		app.UseTransforms();

		app.RegisterSystemsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
		app.RegisterComponentsFromAssembly(typeof(NGame2DDefaultInstaller).Assembly);
	}
}
