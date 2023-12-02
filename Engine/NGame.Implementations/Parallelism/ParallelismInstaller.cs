using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGame.Parallelism;
using NGame.UpdateLoop;

namespace NGame.Core.Parallelism;

public static class ParallelismInstaller
{
	public static IHostApplicationBuilder AddSequentialParallelism(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<OperationRequestScheduler>();
		builder.Services.AddSingleton<IOperationRequestScheduler>(
			services => services.GetRequiredService<OperationRequestScheduler>());
		builder.Services.AddSingleton<IUpdatable>(
			services => services.GetRequiredService<OperationRequestScheduler>());

		builder.Services.AddSingleton<ITaskScheduler, SequentialTaskScheduler>();

		return builder;
	}


	public static IHostApplicationBuilder AddParallelism(this IHostApplicationBuilder builder)
	{
		builder.Services.AddSingleton<OperationRequestScheduler>();
		builder.Services.AddSingleton<IOperationRequestScheduler>(
			services => services.GetRequiredService<OperationRequestScheduler>());
		builder.Services.AddSingleton<IUpdatable>(
			services => services.GetRequiredService<OperationRequestScheduler>());

		builder.Services.AddSingleton<ITaskScheduler, ParallelTaskScheduler>();

		return builder;
	}
}
