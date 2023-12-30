using System.Net;
using System.Net.Sockets;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IFreePortFinder
{
	int GetAvailablePort(IPAddress ipAddress);
}



public class FreePortFinder : IFreePortFinder
{
	public int GetAvailablePort(IPAddress ipAddress)
	{
		var defaultLoopbackEndpoint = new IPEndPoint(ipAddress, port: 0);

		using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Bind(defaultLoopbackEndpoint);

		var boundEndPoint = socket.LocalEndPoint!;
		var ipEndPoint = (IPEndPoint)boundEndPoint;

		return ipEndPoint.Port;
	}
}
