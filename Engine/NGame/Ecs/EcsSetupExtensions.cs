using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;
using NGame.UpdateLoop;

namespace NGame.Ecs;



public static class EcsSetupExtensions
{
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
}
