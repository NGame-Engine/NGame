using Microsoft.Extensions.Logging;
using ServiceWire.TcpIp;

namespace NGameEditor.Backend.Ipc.Setup;



public interface IIpcRunner
{
	void Start();
}



public class IpcRunner : IIpcRunner, IDisposable
{
	private readonly ILogger<IpcRunner> _logger;
	private readonly TcpHost _tcpHost;


	public IpcRunner(
		ILogger<IpcRunner> logger,
		TcpHost tcpHost
	)
	{
		_logger = logger;
		_tcpHost = tcpHost;
	}


	public void Start()
	{
		_tcpHost.Open();

		_logger.LogInformation(
			"{StartedMessage}",
			Bridge.BridgeConventions.ProcessStartedMessage
		);
	}


	public void Dispose()
	{
		_logger.LogInformation("Stopping IcpRunner...");
		_tcpHost.Dispose();
		_logger.LogInformation("IcpRunner stopped");
	}
}
