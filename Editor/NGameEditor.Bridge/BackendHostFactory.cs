using System.Net;
using ServiceWire;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge;



public interface IBackendHostFactory
{
	Host Create(IPEndPoint ipEndPoint, IBackendApi backendApi);
}



public class BackendHostFactory : IBackendHostFactory
{
	public Host Create(IPEndPoint ipEndPoint, IBackendApi backendApi)
	{
		var tcpHost = new TcpHost(ipEndPoint);
		tcpHost.AddService(backendApi);

		return tcpHost;
	}
}
