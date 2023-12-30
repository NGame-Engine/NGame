using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.InterProcessCommunication;



public class BackendHostedService(
	ILogger<BackendHostedService> logger,
	IBackendApi backendApi,
	IHostRunner hostRunner,
	IEnumerable<IBackendStartListener> backendStartListeners
) : IHostedService
{
	public Task StartAsync(CancellationToken cancellationToken)
	{
		hostRunner.AddService(backendApi);
		hostRunner.Open();

		var port = hostRunner.Port;
		var startedMessage = $"{BridgeConventions.ProcessStartedMessage}{port}";
		logger.LogInformation("{StartedMessage}", startedMessage);

		foreach (var backendStartListener in backendStartListeners.OrderBy(x => x.Priority))
		{
			backendStartListener.OnBackendStarted();
		}

		return Task.CompletedTask;
	}


	public Task StopAsync(CancellationToken cancellationToken)
	{
		hostRunner.Close();

		return Task.CompletedTask;
	}
}
