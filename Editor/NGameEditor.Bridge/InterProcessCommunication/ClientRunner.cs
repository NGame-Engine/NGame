using System.Net;
using NGameEditor.Results;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IClientRunner<TService> where TService : class
{
	void StartClient(int port);
	void CloseCurrentClient();
	Result<TService> GetClientService();
}



public class ClientRunner<TService> :
	IClientRunner<TService>, IDisposable
	where TService : class
{
	private TcpClient<TService>? TcpClient { get; set; }


	public void StartClient(int port)
	{
		CloseCurrentClient();

		var ipEndPoint = new IPEndPoint(IPAddress.Loopback, port);
		TcpClient = new TcpClient<TService>(ipEndPoint);
	}


	public void CloseCurrentClient()
	{
		var tcpClient = TcpClient;
		TcpClient = null;
		tcpClient?.Dispose();
	}


	public Result<TService> GetClientService() =>
		TcpClient == null
			? Result.Error("Client not running")
			: Result.Success(TcpClient.Proxy);


	void IDisposable.Dispose()
	{
		CloseCurrentClient();
	}
}
