using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Ecs.Events;

namespace NGame.Ecs;



public static class EcsInstaller
{
	public static INGameApplicationBuilder AddComponentSystem(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ISystemCollection, SystemCollection>();
		builder.Services.AddSingleton<IComponentTypeRegistry, ComponentTypeRegistry>();

		builder.Services.AddSingleton<ISceneEventBus, SceneEventBus>();
		builder.Services.AddSingleton<IEntityEventBus, EntityEventBus>();
		builder.Services.AddSingleton<ITransformEventBus, TransformEventBus>();

		builder.Services.AddSingleton<IEcsTypeFactory, EcsTypeFactory>();

		builder.Services.AddTransient<EventConnector>();

		builder.AddComponentsFromAssembly(typeof(EcsInstaller).Assembly);

		return builder;
	}


	public static NGameApplication UseComponentSystem(this NGameApplication app)
	{
		var eventConnector = app.Services.GetRequiredService<EventConnector>();
		eventConnector.ConnectEvents();

		return app;
	}


	public static INGameApplicationBuilder AddComponentsFromAssembly(
		this INGameApplicationBuilder builder,
		Assembly assembly
	)
	{
		var concreteComponentTypes =
			assembly
				.GetExportedTypes()
				.Where(
					x =>
						!x.IsAbstract &&
						x.IsAssignableTo(typeof(Component))
				);

		foreach (var componentType in concreteComponentTypes)
		{
			builder.Services.AddTransient(componentType);
		}

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
		where T : Component
	{
		var componentTypeRegistry = app.Services.GetRequiredService<IComponentTypeRegistry>();
		componentTypeRegistry.Register<T>();

		return app;
	}
}
