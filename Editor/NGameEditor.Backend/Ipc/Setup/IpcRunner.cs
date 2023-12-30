using Microsoft.Extensions.Logging;
using ServiceWire;

namespace NGameEditor.Backend.Ipc.Setup;



public interface IIpcRunner
{
	void Start();
}



public class IpcRunner(
	ILogger<IpcRunner> logger,
	Host tcpHost
) : IIpcRunner, IDisposable
{
	public void Start()
	{
		tcpHost.Open();

		logger.LogInformation(
			"{StartedMessage}",
			Bridge.BridgeConventions.ProcessStartedMessage
		);
	}


	public void Dispose()
	{
		logger.LogInformation("Stopping IcpRunner...");
		tcpHost.Dispose();
		logger.LogInformation("IcpRunner stopped");
	}
}
