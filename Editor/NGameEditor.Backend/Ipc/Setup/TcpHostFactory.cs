using NGameEditor.Backend.Setup.ApplicationConfigurations;
using NGameEditor.Bridge;
using ServiceWire;
using ServiceWire.TcpIp;

namespace NGameEditor.Backend.Ipc.Setup;



public interface ITcpHostFactory
{
	Host Create();
}



public class TcpHostFactory(
	IBackendService backendService,
	ApplicationConfiguration applicationConfiguration
)
	: ITcpHostFactory
{
	public Host Create()
	{
		var ipEndPoint = applicationConfiguration.BackendIpEndPoint;
		var tcpHost = new TcpHost(ipEndPoint);
		tcpHost.AddService(backendService);
		return tcpHost;
	}
}
