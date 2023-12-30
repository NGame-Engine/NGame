using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Results;

namespace NGameEditor.Functionality.Scenes;



public interface ISceneSaver
{
	void SaveCurrentScene();
}



public class SceneSaver(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<SceneSaver> logger
) : ISceneSaver
{
	public void SaveCurrentScene() =>
		clientRunner
			.GetClientService()
			.Then(x => x.SaveCurrentScene())
			.Then(() => logger.LogInformation("Scene saved"))
			.IfError(logger.Log);
}
