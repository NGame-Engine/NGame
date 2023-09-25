using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.Ecs;

namespace NGame.Setup;

public sealed class NGameHostedService : IHostedService
{
	private readonly ILogger _logger;
	private readonly ISystemCollection _systemCollection;


	public NGameHostedService(
		ILogger<NGameHostedService> logger,
		IHostApplicationLifetime appLifetime,
		ISystemCollection systemCollection
	)
	{
		_logger = logger;
		_systemCollection = systemCollection;

		appLifetime.ApplicationStarted.Register(OnStarted);
		appLifetime.ApplicationStopping.Register(OnStopping);
		appLifetime.ApplicationStopped.Register(OnStopped);
	}


	public Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("1. StartAsync has been called.");

		return Task.CompletedTask;
	}


	public async Task RunGameAsync(CancellationToken cancellationToken)
	{
		var loopCounter = 0;

		while (loopCounter < 5)
		{
			loopCounter++;

			await _systemCollection.UpdateSystems(cancellationToken);
		}
	}


	public Task StopAsync(CancellationToken cancellationToken)
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
