using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Components.Audio;

namespace NGamePlugin.Audio.Sfml;



public static class SfmlAudioInstaller
{
	public static void AddSfmlAudio(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IAudioPlugin, SfmlAudioPlugin>();
	}


	public static void UseSfmlAudio(this NGameApplication app)
	{
	}
}
