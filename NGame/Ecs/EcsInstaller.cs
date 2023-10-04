using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Ecs.Events;
using NGame.UpdateSchedulers;

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

		builder.Services.AddSingleton<ITagRetriever, TagRetriever>();

		builder.AddComponentsFromAssembly(typeof(EcsInstaller).Assembly);

		return builder;
	}


	public static NGameApplication UseComponentSystem(this NGameApplication app)
	{
		var eventConnector = app.Services.GetRequiredService<EventConnector>();
		eventConnector.ConnectEvents();

		return app;
	}


	public static INGameApplicationBuilder AddSystemsFromAssembly(
		this INGameApplicationBuilder builder,
		Assembly assembly
	)
	{
		foreach (var systemType in GetConcreteSubTypes<ISystem>(assembly))
		{
			builder.Services.AddSingleton(systemType);
		}

		return builder;
	}


	public static INGameApplicationBuilder AddComponentsFromAssembly(
		this INGameApplicationBuilder builder,
		Assembly assembly
	)
	{
		foreach (var componentType in GetConcreteSubTypes<Component>(assembly))
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


	public static NGameApplication RegisterSystemsFromAssembly(
		this NGameApplication app,
		Assembly assembly
	)
	{
		var systemCollection = app.Services.GetRequiredService<ISystemCollection>();
		var updatableCollection = app.Services.GetRequiredService<IUpdatableCollection>();
		var drawableCollection = app.Services.GetRequiredService<IDrawableCollection>();
		foreach (var type in GetConcreteSubTypes<ISystem>(assembly))
		{
			var service = app.Services.GetService(type);
			if (service is not ISystem system) continue;

			systemCollection.Add(system);

			if (system is IUpdatable updatable) updatableCollection.Add(updatable);
			if (system is IDrawable drawable) drawableCollection.Add(drawable);
		}


		return app;
	}


	public static NGameApplication RegisterComponent<T>(this NGameApplication app)
		where T : Component
	{
		var componentTypeRegistry = app.Services.GetRequiredService<IComponentTypeRegistry>();
		componentTypeRegistry.Register<T>();

		return app;
	}


	public static NGameApplication RegisterComponentsFromAssembly(
		this NGameApplication app,
		Assembly assembly
	)
	{
		var componentTypeRegistry = app.Services.GetRequiredService<IComponentTypeRegistry>();
		foreach (var type in GetConcreteSubTypes<Component>(assembly))
		{
			componentTypeRegistry.Register(type);
		}

		return app;
	}


	private static IEnumerable<Type> GetConcreteSubTypes<T>(Assembly assembly) =>
		assembly
			.GetExportedTypes()
			.Where(
				x =>
					!x.IsAbstract &&
					x.IsAssignableTo(typeof(T))
			);
}
