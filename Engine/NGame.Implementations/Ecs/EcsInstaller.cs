using Microsoft.Extensions.DependencyInjection;
using NGame.Ecs;
using NGame.Setup;
using NGame.UpdateLoop;

namespace NGame.Core.Ecs;



public static class EcsInstaller
{
	public static INGameBuilder AddEcs(this INGameBuilder builder)
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
