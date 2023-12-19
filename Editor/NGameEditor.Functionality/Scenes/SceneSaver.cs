using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Results;

namespace NGameEditor.Functionality.Scenes;



public interface ISceneSaver
{
	void SaveCurrentScene();
}



public class SceneSaver(
	IBackendRunner backendRunner,
	ILogger<SceneSaver> logger
) : ISceneSaver
{
	public void SaveCurrentScene() =>
		backendRunner
			.GetBackendService()
			.Then(x => x.SaveCurrentScene())
			.Then(() => logger.LogInformation("Scene saved"))
			.IfError(logger.Log);
}
