using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGame.UpdateLoop;



public static class NGameBuilderExtensions
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder RegisterUpdatable<T>(this IHostApplicationBuilder builder, int? orderBy = null)
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


	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder RegisterDrawable<T>(this IHostApplicationBuilder builder, int? orderBy = null)
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
