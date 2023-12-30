using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.InterProcessCommunication;



public class BackendHostedService(
	ILogger<BackendHostedService> logger,
	IBackendApi backendApi,
	IHostRunner hostRunner
) : IHostedService
{
	public Task StartAsync(CancellationToken cancellationToken)
	{
		hostRunner.AddService(backendApi);
		hostRunner.Open();

		var port = hostRunner.Port;
		var startedMessage = $"{BridgeConventions.ProcessStartedMessage}{port}";
		logger.LogInformation("{StartedMessage}", startedMessage);

		return Task.CompletedTask;
	}


	public Task StopAsync(CancellationToken cancellationToken)
	{
		hostRunner.Close();

		return Task.CompletedTask;
	}
}
