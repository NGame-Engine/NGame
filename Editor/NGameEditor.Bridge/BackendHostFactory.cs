using System.Net;
using ServiceWire;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge;



public interface IBackendHostFactory
{
	Host Create(IPEndPoint ipEndPoint, IBackendService backendService);
}



public class BackendHostFactory : IBackendHostFactory
{
	public Host Create(IPEndPoint ipEndPoint, IBackendService backendService)
	{
		var tcpHost = new TcpHost(ipEndPoint);
		tcpHost.AddService(backendService);

		return tcpHost;
	}
}
