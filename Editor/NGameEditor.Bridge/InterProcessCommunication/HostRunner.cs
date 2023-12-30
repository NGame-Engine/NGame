using ServiceWire.TcpIp;

namespace NGameEditor.Bridge.InterProcessCommunication;





public interface IHostRunner
{
	int Port { get; }

	void AddService<TService>(TService service) where TService : class;
	void Open();
	void Close();
}



public class HostRunner(TcpHost tcpHost) : IHostRunner
{
	public int Port => tcpHost.EndPoint.Port;


	public void AddService<TService>(TService service) where TService : class
	{
		tcpHost.AddService(service);
	}


	public void Open()
	{
		tcpHost.Open();
	}


	public void Close()
	{
		tcpHost.Close();
	}
}
