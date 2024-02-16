using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NGame.Platform.UpdateLoop;



public static class UpdateLoopInstaller
{
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static IHostApplicationBuilder AddPlatformUpdateLoop(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ITickScheduler, TickScheduler>();
		builder.Services.AddTransient<IRenderContext, NoOpRenderContext>();

		return builder;
	}
}
