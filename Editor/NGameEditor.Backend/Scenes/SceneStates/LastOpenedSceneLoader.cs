using NGameEditor.Backend.Configurations.UserDatas;
using NGameEditor.Backend.Projects;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public interface ILastOpenedSceneLoader
{
	Result<BackendScene> GetLastOpenedScene();
}



public class LastOpenedSceneLoader : ILastOpenedSceneLoader
{
	private readonly ProjectDefinition _projectDefinition;
	private readonly IUserDataSerializer _userDataSerializer;
	private readonly ISceneFileWatcher _sceneFileWatcher;
	private readonly ISceneFileReader _sceneFileReader;


	public LastOpenedSceneLoader(
		ProjectDefinition projectDefinition,
		IUserDataSerializer userDataSerializer,
		ISceneFileWatcher sceneFileWatcher,
		ISceneFileReader sceneFileReader
	)
	{
		_projectDefinition = projectDefinition;
		_userDataSerializer = userDataSerializer;
		_sceneFileWatcher = sceneFileWatcher;
		_sceneFileReader = sceneFileReader;
	}


	public Result<BackendScene> GetLastOpenedScene()
	{
		var userDataFilePath = _projectDefinition.GetUserDataFilePath();

		if (File.Exists(userDataFilePath.Path) == false)
		{
			return Result.Error($"No user data found at '{userDataFilePath.Path}'");
		}

		var allText = File.ReadAllText(userDataFilePath.Path);
		var userData = _userDataSerializer.Deserialize(allText);

		var lastOpenedProject = userData.LastOpenedScene;
		if (lastOpenedProject == Guid.Empty)
		{
			return Result.Error("Never opened a scene before");
		}

		return _sceneFileWatcher
			.GetPathToSceneFile(lastOpenedProject)
			.Then(_sceneFileReader.ReadSceneFile);
	}



}
