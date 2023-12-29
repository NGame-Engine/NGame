using System.Net;
using System.Net.Sockets;
using NGameEditor.Bridge.Setup;
using NGameEditor.Results;
using ServiceWire.TcpIp;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IBackendRunner
{
	Task<Result<IBackendService>> StartBackend(ProjectId projectId);
	Result CloseCurrentBackend();
	Result<IBackendService> GetBackendService();
}



public class BackendRunner(
	IBackendProcessRunner backendProcessRunner,
	ISolutionConfigurationReader solutionConfigurationReader
)
	: IBackendRunner
{
	public IBackendService? BackendService => TcpClient?.Proxy;
	private TcpClient<IBackendService>? TcpClient { get; set; }


	public async Task<Result<IBackendService>> StartBackend(ProjectId projectId)
	{
		CloseCurrentBackend();

		var solutionFilePath = projectId.SolutionFilePath;
		var solutionConfigurationResult = solutionConfigurationReader.Read(solutionFilePath);
		if (solutionConfigurationResult.HasError)
		{
			return Result.Error(solutionConfigurationResult.ErrorValue!);
		}

		var solutionFolder = solutionFilePath.GetParentDirectory()!;
		var solutionConfigurationJsonModel = solutionConfigurationResult.SuccessValue!;
		var relativeEditorProjectPath = solutionConfigurationJsonModel.EditorProjectFile;
		var editorProjectFile = solutionFolder.CombineWith(relativeEditorProjectPath);


		var ipEndPoint = GetIpEndpoint();
		await backendProcessRunner.StartNewProcess(editorProjectFile, ipEndPoint, projectId);

		TcpClient = new TcpClient<IBackendService>(ipEndPoint);
		var backendService = TcpClient.Proxy;

		return Result.Success(backendService);
	}


	public Result CloseCurrentBackend()
	{
		var tcpClient = TcpClient;
		TcpClient = null;
		tcpClient?.Dispose();

		backendProcessRunner.StopCurrentProcess();

		return Result.Success();
	}


	public Result<IBackendService> GetBackendService() =>
		BackendService == null
			? Result.Error("Backend service not running")
			: Result.Success(BackendService);


	private static IPEndPoint GetIpEndpoint()
	{
		var defaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, port: 0);
		using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Bind(defaultLoopbackEndpoint);
		var boundEndPoint = socket.LocalEndPoint!;
		var ipEndPoint = (IPEndPoint)boundEndPoint;
		var availablePort = ipEndPoint.Port;
		return new IPEndPoint(IPAddress.Loopback, availablePort);
	}
}
