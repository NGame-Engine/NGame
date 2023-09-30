using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Lines;



public static class LineInstaller
{
	public static INGameApplicationBuilder AddLines(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<LineRendererSystem>();

		return builder;
	}


	public static NGameApplication UseLines(this NGameApplication app)
	{
		app.RegisterComponent<LineRenderer>();
		app.UseDrawable<LineRendererSystem>();
		app.RegisterSystem<LineRendererSystem>();


		return app;
	}
}
