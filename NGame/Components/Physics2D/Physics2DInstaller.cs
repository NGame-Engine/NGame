using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Ecs;
using NGame.UpdateSchedulers;

namespace NGame.Components.Physics2D;



public static class Physics2DInstaller
{
	public static INGameApplicationBuilder AddPhysics2D(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<Physics2DSystem>();

		return builder;
	}


	public static NGameApplication UsePhysics2D(this NGameApplication app)
	{
		app.RegisterComponent<BoxCollider2D>();
		app.RegisterComponent<CircleCollider2D>();
		app.RegisterComponent<PhysicsBody2D>();

		app.UseUpdatable<Physics2DSystem>();
		app.RegisterSystem<Physics2DSystem>();


		return app;
	}
}
