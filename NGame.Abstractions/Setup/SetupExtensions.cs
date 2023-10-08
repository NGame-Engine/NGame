using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NGame.Components;
using NGame.Services;
using NGame.Systems;

namespace NGame.Setup;



public static class SetupExtensions
{
	public static INGameBuilder AddSystemsFromAssembly(
		this INGameBuilder builder,
		Assembly assembly
	)
	{
		foreach (var systemType in GetConcreteSubTypes<ISystem>(assembly))
		{
			builder.Services.AddSingleton(systemType);
		}

		return builder;
	}


	public static INGameBuilder AddComponentsFromAssembly(
		this INGameBuilder builder,
		Assembly assembly
	)
	{
		foreach (var componentType in GetConcreteSubTypes<Component>(assembly))
		{
			builder.Services.AddTransient(componentType);
		}

		return builder;
	}


	public static INGame RegisterSystem<T>(this INGame app)
		where T : ISystem
	{
		var systemCollection = app.Services.GetRequiredService<ISystemCollection>();
		var system = app.Services.GetRequiredService<T>();
		systemCollection.AddSystem(system);
		return app;
	}


	public static INGame RegisterSystemsFromAssembly(
		this INGame app,
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

			systemCollection.AddSystem(system);

			if (system is IUpdatable updatable) updatableCollection.Add(updatable);
			if (system is IDrawable drawable) drawableCollection.Add(drawable);
		}


		return app;
	}


	public static INGame RegisterComponent<T>(this INGame app)
		where T : Component
	{
		var componentTypeRegistry = app.Services.GetRequiredService<IComponentTypeRegistry>();
		componentTypeRegistry.Register<T>();

		return app;
	}


	public static INGame RegisterComponentsFromAssembly(
		this INGame app,
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


	public static INGame UseUpdatable<T>(this INGame app, int? orderBy = null)
		where T : IUpdatable
	{
		var updatableCollection = app.Services.GetRequiredService<IUpdatableCollection>();
		var updatable = app.Services.GetRequiredService<T>();
		updatable.Order = orderBy ?? updatable.Order;
		updatableCollection.Add(updatable);
		return app;
	}


	public static INGame UseDrawable<T>(this INGame app, int? orderBy = null)
		where T : IDrawable
	{
		var drawableCollection = app.Services.GetRequiredService<IDrawableCollection>();
		var drawable = app.Services.GetRequiredService<T>();
		drawable.Order = orderBy ?? drawable.Order;
		drawableCollection.Add(drawable);
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
