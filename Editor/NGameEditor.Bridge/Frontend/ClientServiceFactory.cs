using System.Net;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge.Frontend;



public interface IClientServiceFactory
{
	TService CreateClientService<TService>(IPEndPoint ipEndPoint) where TService : class;
}



public class ClientServiceFactory : IClientServiceFactory
{
	public TService CreateClientService<TService>(IPEndPoint ipEndPoint) where TService : class
	{
		var tcpClient = new TcpClient<TService>(ipEndPoint);
		return tcpClient.Proxy;
	}
}
