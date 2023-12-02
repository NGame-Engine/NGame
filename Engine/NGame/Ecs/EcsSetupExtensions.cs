using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.UpdateLoop;

namespace NGame.Ecs;



public static class EcsSetupExtensions
{
	public static IHostApplicationBuilder RegisterSystem<T>(this IHostApplicationBuilder builder)
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
}
