using Microsoft.Extensions.DependencyInjection;
using NGame.Application;

namespace NGame.UpdateSchedulers;

public static class NGameApplicationExtensions
{
	public static INGameApplicationBuilder AddUpdateScheduler(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IUpdateScheduler, UpdateScheduler>();
		builder.Services.AddSingleton<IUpdatableCollection, UpdatableCollection>();
		builder.Services.AddSingleton<IDrawableCollection, DrawableCollection>();

		return builder;
	}


	public static NGameApplication UseUpdatable<T>(this NGameApplication app)
		where T : IUpdatable
	{
		var systemCollection = app.Services.GetRequiredService<IUpdatableCollection>();
		var system = app.Services.GetRequiredService<T>();
		systemCollection.Add(system);
		return app;
	}


	public static NGameApplication UseDrawable<T>(this NGameApplication app)
		where T : IDrawable
	{
		var systemCollection = app.Services.GetRequiredService<IDrawableCollection>();
		var system = app.Services.GetRequiredService<T>();
		systemCollection.Add(system);
		return app;
	}
}
