using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;
using NGameEditor.Results;

namespace NGameEditor.Functionality.InterProcessCommunication;



public interface IBackendStarter
{
	Task<Result<IBackendApi>> StartBackend(ProjectId projectId);
}



public class BackendStarter(
	IBackendProcessRunner backendProcessRunner,
	ISolutionConfigurationReader solutionConfigurationReader,
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


		var backendPort = await backendProcessRunner.StartNewProcess(
			editorProjectFile,
			solutionFilePath
		);

		clientRunner.StartClient(backendPort);
		return clientRunner.GetClientService();
	}
}
