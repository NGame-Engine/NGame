using Microsoft.Extensions.DependencyInjection;
using NGame.Application;

namespace NGame.Components.Audio;



public static class AudioInstaller
{
	public static INGameApplicationBuilder AddAudio(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IGlobalSoundPlayer, GlobalSoundPlayer>();

		return builder;
	}


	// TODO probably move to platform, it's implementation specific
	public static NGameApplication UseAudio(this NGameApplication app)
	{
		var audioPlugin = app.Services.GetService<IAudioPlugin>();
		if (audioPlugin == null)
		{
			throw new InvalidOperationException(
				"Trying to use audio, but no audio plugin registered"
			);
		}

		var applicationEvents = app.Services.GetRequiredService<IApplicationEvents>();
		applicationEvents.GameLoopStopped += (_, _) => audioPlugin.UnloadAllClips();
		applicationEvents.GameLoopStopped += (_, _) => audioPlugin.RemoveAllSources();

		return app;
	}
}
