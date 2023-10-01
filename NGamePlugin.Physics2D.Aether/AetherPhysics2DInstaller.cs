using Microsoft.Extensions.DependencyInjection;
using NGame.Application;
using NGame.Components.Physics2D;
using nkast.Aether.Physics2D.Dynamics;

namespace NGamePlugin.Physics2D.Aether;



public static class AetherPhysics2DInstaller
{
	public static INGameApplicationBuilder AddAetherPhysics2D(this INGameApplicationBuilder builder)
	{
		builder.Services.AddSingleton<World>();
		builder.Services.AddSingleton<IPhysicsEngine2D, AetherPhysicsEngine2D>();
		return builder;
	}


	public static NGameApplication UseAetherPhysics2D(this NGameApplication app)
	{
		return app;
	}
}
