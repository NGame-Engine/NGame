using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;
using NGameEditor.Results;
using ServiceWire.TcpIp;

namespace NGameEditor.Functionality.InterProcessCommunication;



public interface IBackendStarter
{
	Task<Result<IBackendApi>> StartBackend(ProjectId projectId);
}



public class BackendStarter(
	IBackendProcessRunner backendProcessRunner,
	ISolutionConfigurationReader solutionConfigurationReader,
	TcpHost host,
	IClientRunner<IBackendApi> clientRunner
)
	: IBackendStarter
{
	public async Task<Result<IBackendApi>> StartBackend(ProjectId projectId)
	{
		clientRunner.CloseCurrentClient();
		backendProcessRunner.StopCurrentProcess();


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
		var frontendPort = frontendIpEndPoint.Port;

		var backendApplicationArguments = new BackendApplicationArguments(frontendPort, solutionFilePath);

		var backendPort = await backendProcessRunner.StartNewProcess(
			editorProjectFile,
			backendApplicationArguments
		);

		clientRunner.StartClient(backendPort);
		return clientRunner.GetClientService();
	}
}
