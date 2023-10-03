using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Application;
using NGame.Components.Audio;
using NGame.Components.Lines;
using NGame.Components.Physics2D;
using NGame.Components.Sprites;
using NGame.Components.Texts;
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

		builder.AddTransforms();

		builder.AddSceneSupport();

		builder.AddSprites();
		builder.AddText();
		builder.AddLines();
		builder.AddAudio();

		builder.AddPhysics2D();
	}


	public static void UseNGame2DDefault(this NGameApplication app)
	{
		app.UseComponentSystem();
		app.UseTransforms();

		app.UseSprites();
		app.UseText();
		app.UseLines();
		app.UseAudio();

		app.UsePhysics2D();
	}
}
