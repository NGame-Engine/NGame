using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Application;
using NGame.Components.Audio;
using NGame.Components.Lines;
using NGame.Components.Sprites;
using NGame.Components.Texts;
using NGame.Ecs;
using NGame.Scenes;
using NGame.UpdateSchedulers;

namespace NGame;



public static class NGameDefaultInstaller
{
	public static void AddNGameDefault(this INGameApplicationBuilder builder)
	{
		if (builder.Environment.IsDevelopment())
		{
			builder.Logging.AddConsole();
		}

		builder.AddUpdateScheduler();
		builder.AddComponentSystem();
		builder.AddSceneSupport();

		builder.AddSprites();
		builder.AddText();
		builder.AddLines();
		builder.AddAudio();
	}


	public static void UseNGameDefault(this NGameApplication app)
	{
		app.UseSprites();
		app.UseText();
		app.UseLines();
		app.UseAudio();
	}
}
