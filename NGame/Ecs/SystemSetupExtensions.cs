using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;

namespace NGame.Ecs;

public static class SystemSetupExtensions
{
	public static INGameApplicationBuilder AddNGameSystemSupport(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ISystemCollection, SystemCollection>();
		return builder;
	}


	public static NGameApplication UseSystem<T>(this NGameApplication app)
		where T : ISystem
	{
		var systemCollection = app.Services.GetRequiredService<ISystemCollection>();
		var system = app.Services.GetRequiredService<T>();
		systemCollection.Add(system);
		return app;
	}
}
