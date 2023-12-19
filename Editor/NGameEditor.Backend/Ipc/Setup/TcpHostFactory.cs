using NGameEditor.Backend.Setup.ApplicationConfigurations;
using NGameEditor.Bridge;
using ServiceWire.TcpIp;

namespace NGameEditor.Backend.Ipc.Setup;



public interface ITcpHostFactory
{
	TcpHost Create();
}



public class TcpHostFactory : ITcpHostFactory
{
	private readonly IBackendService _backendService;
	private readonly ApplicationConfiguration _applicationConfiguration;


	public TcpHostFactory(IBackendService backendService, ApplicationConfiguration applicationConfiguration)
	{
		_backendService = backendService;
		_applicationConfiguration = applicationConfiguration;
	}


	public TcpHost Create()
	{
		var ipEndPoint = _applicationConfiguration.IpEndPoint;
		var tcpHost = new TcpHost(ipEndPoint);
		tcpHost.AddService(_backendService);
		return tcpHost;
	}
}
