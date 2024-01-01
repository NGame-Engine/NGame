using NGameEditor.Backend.Configurations.UserDatas;
using NGameEditor.Backend.Projects;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public interface ILastOpenedSceneLoader
{
	Result<BackendScene> GetLastOpenedScene();
}



public class LastOpenedSceneLoader(
	ProjectDefinition projectDefinition,
	IUserDataSerializer userDataSerializer,
	ISceneFileWatcher sceneFileWatcher,
	ISceneFileReader sceneFileReader
)
	: ILastOpenedSceneLoader
{
	public Result<BackendScene> GetLastOpenedScene()
	{
		var userDataFilePath = projectDefinition.GetUserDataFilePath();

		if (File.Exists(userDataFilePath.Path) == false)
		{
			return Result.Error($"No user data found at '{userDataFilePath.Path}'");
		}

		var allText = File.ReadAllText(userDataFilePath.Path);
		var userData = userDataSerializer.Deserialize(allText);

		var lastOpenedProject = userData.LastOpenedScene;
		if (lastOpenedProject == Guid.Empty)
		{
			return Result.Error("Never opened a scene before");
		}

		return sceneFileWatcher
			.GetPathToSceneFile(lastOpenedProject)
			.Then(sceneFileReader.ReadSceneFile);
	}



}
