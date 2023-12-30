using System.Net;
using NGameEditor.Bridge.InterProcessCommunication;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge;



public interface IFrontendTcpHostFactory
{
	TcpHost Create();
}



public class FrontendTcpHostFactory(
	IFreePortFinder freePortFinder,
	IFrontendApi frontendApi
) : IFrontendTcpHostFactory
{
	public TcpHost Create()
	{
		var ipAddress = IPAddress.Loopback;

		var freePort = freePortFinder.GetAvailablePort(ipAddress);
		var ipEndPoint = new IPEndPoint(ipAddress, freePort);
		var tcpHost = new TcpHost(ipEndPoint);

		tcpHost.AddService(frontendApi);
		tcpHost.Open();

		return tcpHost;
	}
}
