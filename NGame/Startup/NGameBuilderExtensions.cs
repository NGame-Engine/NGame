using Microsoft.Extensions.DependencyInjection;
using NGame.Assets;
using NGame.Components;
using NGame.Ecs;
using NGame.Services;

namespace NGame.Startup;



public static class NGameBuilderExtensions
{
	public static INGameBuilder AddAssetType<T>(this INGameBuilder builder)
		where T : Asset
	{
		builder.ApplicationStarting += app =>
		{
			var typeRegistry = app.Services.GetRequiredService<ITypeRegistry>();
			typeRegistry.AddType<Asset, T>();
		};

		return builder;
	}


	public static INGameBuilder AddComponentType<T>(this INGameBuilder builder)
		where T : Component
	{
		builder.ApplicationStarting += app =>
		{
			var typeRegistry = app.Services.GetRequiredService<ITypeRegistry>();
			typeRegistry.AddType<Component, T>();
		};

		return builder;
	}


	public static INGameBuilder RegisterSystem<T>(this INGameBuilder builder)
		where T : class, ISystem
	{
		builder.Services.AddSingleton<T>();

		builder.ApplicationStarting += app =>
		{
			var systemCollection = app.Services.GetRequiredService<ISystemCollection>();
			var system = app.Services.GetRequiredService<T>();
			systemCollection.AddSystem(system);

			if (system is IUpdatable updatable)
			{
				var updatableCollection = app.Services.GetRequiredService<IUpdatableCollection>();
				updatableCollection.Add(updatable);
			}

			if (system is IDrawable drawable)
			{
				var drawableCollection = app.Services.GetRequiredService<IDrawableCollection>();
				drawableCollection.Add(drawable);
			}
		};

		return builder;
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
}
