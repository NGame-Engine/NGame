using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Audio;



public static class AudioInstaller
{
	public static INGameApplicationBuilder AddAudio(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<AudioListenerSystem>();
		builder.Services.AddSingleton<AudioSourceSystem>();

		return builder;
	}


	public static NGameApplication UseAudio(this NGameApplication app)
	{
		app.RegisterComponent<AudioListener>();
		app.RegisterSystem<AudioListenerSystem>();
		app.UseUpdatable<AudioListenerSystem>();

		app.RegisterComponent<AudioSource>();
		app.RegisterSystem<AudioSourceSystem>();
		app.UseUpdatable<AudioSourceSystem>();

		return app;
	}
}
