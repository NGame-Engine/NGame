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

		builder.Services.AddSingleton<ISystem>(services => services.GetRequiredService<T>());

		if (typeof(T).IsAssignableTo(typeof(IUpdatable)))
		{
			builder.Services.AddSingleton<IUpdatable>(services => (IUpdatable)services.GetRequiredService<T>());
		}

		if (typeof(T).IsAssignableTo(typeof(IDrawable)))
		{
			builder.Services.AddSingleton<IDrawable>(services => (IDrawable)services.GetRequiredService<T>());
		}

		return builder;
	}


	public static INGameBuilder RegisterUpdatable<T>(this INGameBuilder builder, int? orderBy = null)
		where T : IUpdatable
	{
		builder.Services.AddSingleton<IUpdatable>(services =>
		{
			var updatable = services.GetRequiredService<T>();
			updatable.Order = orderBy ?? updatable.Order;
			return updatable;
		});

		return builder;
	}


	public static INGameBuilder RegisterDrawable<T>(this INGameBuilder builder, int? orderBy = null)
		where T : IDrawable
	{
		builder.Services.AddSingleton<IDrawable>(services =>
		{
			var drawable = services.GetRequiredService<T>();
			drawable.Order = orderBy ?? drawable.Order;
			return drawable;
		});

		return builder;
	}
}
