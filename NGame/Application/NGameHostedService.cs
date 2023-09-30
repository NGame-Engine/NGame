using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGame.UpdateSchedulers;

namespace NGame.Application;

public sealed class NGameHostedService : IHostedService
{
	private readonly ILogger _logger;
	private readonly IUpdateScheduler _updateScheduler;


	public NGameHostedService(
		ILogger<NGameHostedService> logger,
		IHostApplicationLifetime appLifetime,
		IUpdateScheduler updateScheduler,
		IApplicationEvents applicationEvents
	)
	{
		_logger = logger;
		_updateScheduler = updateScheduler;

		appLifetime.ApplicationStarted.Register(OnStarted);
		appLifetime.ApplicationStopping.Register(OnStopping);
		appLifetime.ApplicationStopped.Register(OnStopped);

		applicationEvents.Closing += (_, _) => IsClosing = true;
	}

	private bool IsClosing { get; set; }

	public Task StartAsync(CancellationToken cancellationToken)
	{
		//_logger.LogInformation("1. StartAsync has been called.");

		return Task.CompletedTask;
	}


	public void RunGame()
	{
		_updateScheduler.Initialize();
		while (!IsClosing)
		{
			_updateScheduler.Tick();
		}
	}


	public Task StopAsync(CancellationToken cancellationToken = default)
	{
		_logger.LogInformation("4. StopAsync has been called.");

		return Task.CompletedTask;
	}


	private void OnStarted()
	{
		//_logger.LogInformation("2. OnStarted has been called.");
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
