using Microsoft.Extensions.DependencyInjection;
using NGame.Setup;
using NGame.UpdateLoop;

namespace NGame.Core.UpdateLoop;



public static class UpdateLoopInstaller
{
	public static INGameBuilder AddUpdateLoop(this INGameBuilder builder)
	{
		builder.Services.AddSingleton<ITickScheduler, TickScheduler>();
		builder.Services.AddTransient<IRenderContext, NoOpRenderContext>();

		return builder;
	}
}
