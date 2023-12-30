using System.Net;
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
	ISolutionConfigurationReader solutionConfigurationReader,
	TcpHost host,
	IFreePortFinder freePortFinder
)
	: IBackendRunner
{
	private IBackendService? BackendService => TcpClient?.Proxy;
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


		var frontendIpEndPoint = host.EndPoint;
		var availablePort = freePortFinder.GetAvailablePort(IPAddress.Loopback);
		var backendIpEndPoint = new IPEndPoint(IPAddress.Loopback, availablePort);

		await backendProcessRunner.StartNewProcess(
			editorProjectFile,
			frontendIpEndPoint,
			backendIpEndPoint,
			projectId
		);

		TcpClient = new TcpClient<IBackendService>(backendIpEndPoint);
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
}
