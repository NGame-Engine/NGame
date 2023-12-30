using System.Net;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IHostRunnerFactory
{
	IHostRunner Create();
}



public class HostRunnerFactory(
	IFreePortFinder freePortFinder
) : IHostRunnerFactory
{
	public IHostRunner Create()
	{
		var ipAddress = IPAddress.Loopback;

		var freePort = freePortFinder.GetAvailablePort(ipAddress);
		var ipEndPoint = new IPEndPoint(ipAddress, freePort);
		var tcpHost = new TcpHost(ipEndPoint);

		return new HostRunner(tcpHost);
	}
}
