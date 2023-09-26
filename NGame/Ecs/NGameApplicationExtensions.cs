using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;

namespace NGame.Ecs;

public static class NGameApplicationExtensions
{
	public static INGameApplicationBuilder AddComponentSystem(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IEntityTracker, EntityTracker>(); 
		builder.Services.AddSingleton<IComponentTypeRegistry, ComponentTypeRegistry>();


		return builder;
	}
	
	public static NGameApplication RegisterComponent<T>(this NGameApplication app)
		where T : Component
	{
		var componentTypeRegistry = app.Services.GetRequiredService<IComponentTypeRegistry>();
		componentTypeRegistry.Register<T>();

		return app;
	}
}
