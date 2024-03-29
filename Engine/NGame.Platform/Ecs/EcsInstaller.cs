using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Platform.Setup;
using NGame.UpdateLoop;

namespace NGame.Platform.Ecs;



public static class EcsInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddEcs(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ActionCache>();
		builder.Services.AddSingleton<IActionCache>(services => services.GetRequiredService<ActionCache>());
		builder.RegisterUpdatable<ActionCache>();

		builder.Services.AddSingleton<ISystemCollection, SystemCollection>();


		builder.Services.AddTransient<IRootSceneAccessor, RootSceneAccessor>();

		builder.Services.AddSingleton<TransformProcessor>();
		builder.RegisterDrawable<TransformProcessor>();
		builder.Services.AddSingleton<ITagRetriever, TagRetriever>();


		builder.Services.AddTransient<IBeforeApplicationStartListener, EntityEventConnector>();

		return builder;
	}
}
