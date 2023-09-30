using Microsoft.Extensions.DependencyInjection;
using NGame.Application;

namespace NGame.Ecs;

public static class NGameApplicationExtensions
{
	public static INGameApplicationBuilder AddComponentSystem(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ISystemCollection, SystemCollection>();
		builder.Services.AddSingleton<IEntityTracker, EntityTracker>();
		builder.Services.AddSingleton<IComponentTypeRegistry, ComponentTypeRegistry>();

		return builder;
	}


	public static NGameApplication RegisterSystem<T>(this NGameApplication app)
		where T : ISystem
	{
		var systemCollection = app.Services.GetRequiredService<ISystemCollection>();
		var system = app.Services.GetRequiredService<T>();
		systemCollection.Add(system);
		return app;
	}


	public static NGameApplication RegisterComponent<T>(this NGameApplication app)
		where T : IComponent
	{
		var componentTypeRegistry = app.Services.GetRequiredService<IComponentTypeRegistry>();
		componentTypeRegistry.Register<T>();

		return app;
	}
}
