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
		builder.Services.AddSingleton<IGlobalSoundPlayer, GlobalSoundPlayer>();

		return builder;
	}


	public static NGameApplication UseAudio(this NGameApplication app)
	{
		var audioPlugin = app.Services.GetService<IAudioPlugin>();
		if (audioPlugin == null)
		{
			throw new InvalidOperationException(
				"Trying to use audio, but no audio plugin registered"
			);
		}

		app.RegisterComponent<AudioListener>();
		app.RegisterSystem<AudioListenerSystem>();
		app.UseUpdatable<AudioListenerSystem>();

		app.RegisterComponent<AudioSource>();
		app.RegisterSystem<AudioSourceSystem>();
		app.UseUpdatable<AudioSourceSystem>();

		var applicationEvents = app.Services.GetRequiredService<IApplicationEvents>();
		applicationEvents.GameLoopStopped += (_, _) => audioPlugin.UnloadAllClips();
		applicationEvents.GameLoopStopped += (_, _) => audioPlugin.RemoveAllSources();

		return app;
	}
}
