using Microsoft.Extensions.DependencyInjection;
using NGame.Parallelism;
using NGame.Setup;
using NGame.UpdateLoop;

namespace NGame.Core.Parallelism;



public static class ParallelismInstaller
{
	public static INGameBuilder AddSequentialParallelism(this INGameBuilder builder)
	{
		builder.Services.AddSingleton<OperationRequestScheduler>();
		builder.Services.AddSingleton<IOperationRequestScheduler>(
			services => services.GetRequiredService<OperationRequestScheduler>());
		builder.Services.AddSingleton<IUpdatable>(
			services => services.GetRequiredService<OperationRequestScheduler>());

		builder.Services.AddSingleton<ITaskScheduler, SequentialTaskScheduler>();

		return builder;
	}


	public static INGameBuilder AddParallelism(this INGameBuilder builder)
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
