using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Components.Audio;
using NGame.Inputs;
using NGame.OsWindows;
using NGame.Renderers;
using NGamePlatform.Desktop.Sfml.Audio;
using NGamePlatform.Desktop.Sfml.Inputs;
using NGamePlatform.Desktop.Sfml.Renderers;
using NGamePlatform.Desktop.Sfml.Window;

namespace NGamePlatform.Desktop.Sfml;



public static class SfmlDesktopPlatformInstaller
{
	public static INGameApplicationBuilder AddSfmlDesktopPlatform(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IAudioPlugin, SfmlAudioPlugin>();


		builder.Services.AddSingleton<INGameRenderer, SfmlRenderer>();

		builder.Services.AddTransient<RenderTextureFactory>();
		builder.Services.AddSingleton(
			services =>
				services
					.GetRequiredService<RenderTextureFactory>()
					.Create()
		);


		builder.Services.AddTransient<RenderWindowFactory>();
		builder.Services.AddSingleton(
			services =>
				services
					.GetRequiredService<RenderWindowFactory>()
					.Create()
		);

		builder.Services.AddSingleton<SfmlWindow>();
		builder.Services.AddTransient<IOsWindow>(
			services => services.GetRequiredService<SfmlWindow>()
		);

		builder.Services.AddSingleton<RawInputListener>();
		builder.Services.AddTransient<IRawInputListener>(
			services => services.GetRequiredService<RawInputListener>()
		);

		builder.Services.AddTransient<RenderWindowEventConnector>();

		return builder;
	}


	public static NGameApplication UseSfmlDesktopPlatform(this NGameApplication app)
	{
		var renderWindowEventConnector = app.Services.GetRequiredService<RenderWindowEventConnector>();
		renderWindowEventConnector.ConnectEvents();

		return app;
	}
}
