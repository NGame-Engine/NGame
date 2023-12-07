using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.UpdateLoop;

namespace NGame.Core.UpdateLoop;



public static class UpdateLoopInstaller
{
	public static IHostApplicationBuilder AddUpdateLoop(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ITickScheduler, TickScheduler>();
		builder.Services.AddTransient<IRenderContext, NoOpRenderContext>();

		return builder;
	}
}
