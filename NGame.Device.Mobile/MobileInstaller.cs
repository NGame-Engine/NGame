using Microsoft.Extensions.DependencyInjection;
using NGame.Services;
using NGame.Services.Parallelism;
using NGame.Setup;

namespace NGame;



public static class MobileInstaller
{
	public static INGameBuilder AddMobile(this INGameBuilder builder)
	{
		builder.Services.AddSingleton<ITaskScheduler, SequentialTaskScheduler>();

		return builder;
	}


	public static INGame UseMobile(this INGame app)
	{
		return app;
	}
}
