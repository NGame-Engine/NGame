using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Texts;

public static class TextInstaller
{
	public static INGameApplicationBuilder AddText(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<TextRendererSystem>();

		return builder;
	}


	public static NGameApplication UseText(this NGameApplication app)
	{
		app.RegisterComponent<TextRenderer>();
		app.UseDrawable<TextRendererSystem>();
		app.RegisterSystem<TextRendererSystem>();


		return app;
	}
}
