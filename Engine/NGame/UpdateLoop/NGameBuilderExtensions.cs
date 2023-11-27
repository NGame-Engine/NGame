using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;

namespace NGame.UpdateLoop;



public static class NGameBuilderExtensions
{
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
