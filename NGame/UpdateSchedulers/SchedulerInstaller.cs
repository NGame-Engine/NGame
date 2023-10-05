using Microsoft.Extensions.DependencyInjection;
using NGame.Application;

namespace NGame.UpdateSchedulers;



public static class SchedulerInstaller
{
	public static INGameApplicationBuilder AddUpdateScheduler(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IUpdateScheduler, UpdateScheduler>();
		builder.Services.AddSingleton<IUpdatableCollection, UpdatableCollection>();
		builder.Services.AddSingleton<IDrawableCollection, DrawableCollection>();

		return builder;
	}


	public static NGameApplication UseUpdatable<T>(this NGameApplication app, int? orderBy = null)
		where T : IUpdatable
	{
		var updatableCollection = app.Services.GetRequiredService<IUpdatableCollection>();
		var updatable = app.Services.GetRequiredService<T>();
		updatable.Order = orderBy ?? updatable.Order;
		updatableCollection.Add(updatable);
		return app;
	}


	public static NGameApplication UseDrawable<T>(this NGameApplication app, int? orderBy = null)
		where T : IDrawable
	{
		var drawableCollection = app.Services.GetRequiredService<IDrawableCollection>();
		var drawable = app.Services.GetRequiredService<T>();
		drawable.Order = orderBy ?? drawable.Order;
		drawableCollection.Add(drawable);
		return app;
	}
}
