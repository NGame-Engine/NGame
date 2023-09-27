using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.UpdaterSchedulers;

namespace NGame.Setup;

public sealed class NGameHostedService : IHostedService
{
	private readonly ILogger _logger;
	private readonly IUpdateScheduler _updateScheduler;


	public NGameHostedService(
		ILogger<NGameHostedService> logger,
		IHostApplicationLifetime appLifetime,
		IUpdateScheduler updateScheduler
	)
	{
		_logger = logger;
		_updateScheduler = updateScheduler;

		appLifetime.ApplicationStarted.Register(OnStarted);
		appLifetime.ApplicationStopping.Register(OnStopping);
		appLifetime.ApplicationStopped.Register(OnStopped);
	}


	public Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("1. StartAsync has been called.");

		return Task.CompletedTask;
	}


	public Task RunGameAsync(CancellationTokenSource cancellationTokenSource, CancellationToken cancellationToken)
	{
		_updateScheduler.Initialize(cancellationTokenSource);
		while (!cancellationToken.IsCancellationRequested)
		{
			_updateScheduler.Tick();
		}

		return Task.CompletedTask;
	}


	public Task StopAsync(CancellationToken cancellationToken = default)
	{
		_logger.LogInformation("4. StopAsync has been called.");

		return Task.CompletedTask;
	}


	private void OnStarted()
	{
		_logger.LogInformation("2. OnStarted has been called.");
	}


	private void OnStopping()
	{
		_logger.LogInformation("3. OnStopping has been called.");
	}


	private void OnStopped()
	{
		_logger.LogInformation("5. OnStopped has been called.");
	}
}
