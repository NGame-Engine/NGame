using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Setup;
using NGameEditor.Functionality.Files;
using NGameEditor.Functionality.Projects;
using NGameEditor.Results;

namespace NGameEditor.Functionality.InterProcessCommunication;



public interface IBackendStarter
{
	Task<Result<IBackendApi>> StartBackend(ProjectId projectId);
	void CloseBackend();
}



public class BackendStarter(
	IBackendProcessRunner backendProcessRunner,
	ISolutionConfigurationReader solutionConfigurationReader,
	IClientRunner<IBackendApi> clientRunner
)
	: IBackendStarter, IDisposable
{
	public async Task<Result<IBackendApi>> StartBackend(ProjectId projectId)
	{
		CloseBackend();


		var solutionFilePath = projectId.SolutionFilePath;
		var solutionCompatibleFilePath = solutionFilePath.ToCompatiblePath();
		var solutionConfigurationResult = solutionConfigurationReader.Read(solutionCompatibleFilePath);
		if (solutionConfigurationResult.HasError)
		{
			return Result.Error(solutionConfigurationResult.ErrorValue!);
		}

		var solutionFolder = solutionFilePath.ParentDirectory;
		var solutionConfigurationJsonModel = solutionConfigurationResult.SuccessValue!;
		var relativeEditorProjectPath = solutionConfigurationJsonModel.EditorProjectFile;
		var editorProjectFile = solutionFolder.CombineFile(relativeEditorProjectPath);


		var backendPort = await backendProcessRunner.StartNewProcess(
			editorProjectFile.ToCompatiblePath(),
			solutionCompatibleFilePath
		);

		clientRunner.StartClient(backendPort);
		return clientRunner.GetClientService();
	}


	public void CloseBackend()
	{
		clientRunner.CloseCurrentClient();
		backendProcessRunner.StopCurrentProcess();
	}


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}


	protected void Dispose(bool disposing)
	{
		if (!disposing) return;
		CloseBackend();
	}
}
