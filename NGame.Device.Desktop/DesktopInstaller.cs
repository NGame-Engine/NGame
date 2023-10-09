using Microsoft.Extensions.DependencyInjection;
using NGame.Services;
using NGame.Services.Parallelism;
using NGame.Setup;

namespace NGame;



public static class DesktopInstaller
{
	public static INGameBuilder AddDesktop(this INGameBuilder builder)
	{
		builder.Services.AddSingleton<ITaskScheduler, ParallelTaskScheduler>();

		return builder;
	}


	public static INGame UseDesktop(this INGame app)
	{
		return app;
	}
}
