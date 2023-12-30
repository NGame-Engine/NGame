using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;

namespace NGameEditor.Backend.Ipc;



public class BackendHostedService(
	ILogger<BackendHostedService> logger,
	IBackendService backendService,
	IBackendHostFactory backendHostFactory,
	IFreePortFinder freePortFinder
) : IHostedService
{
	private ServiceWire.Host? Host { get; set; }
	
	public Task StartAsync(CancellationToken cancellationToken)
	{
		var availablePort = freePortFinder.GetAvailablePort(IPAddress.Loopback);
		var ipEndPoint = new IPEndPoint(IPAddress.Loopback, availablePort);
		Host = backendHostFactory.Create(ipEndPoint, backendService);
		Host.Open();

		var port = ipEndPoint.Port;
		var startedMessage = $"{BridgeConventions.ProcessStartedMessage}{port}";
		logger.LogInformation("{StartedMessage}", startedMessage);

		return Task.CompletedTask;
	}


	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
