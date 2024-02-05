using NGameEditor.Backend.Configurations.UserDatas;
using NGameEditor.Backend.Files;
using NGameEditor.Backend.Projects;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes.SceneStates;



public interface ILastOpenedSceneLoader
{
	Result<BackendScene> GetLastOpenedScene();
}



internal class LastOpenedSceneLoader(
	ProjectDefinition projectDefinition,
	IUserDataSerializer userDataSerializer,
	ISceneFileReader sceneFileReader,
	IAssetFileWatcher assetFileWatcher
)
	: ILastOpenedSceneLoader
{
	public Result<BackendScene> GetLastOpenedScene()
	{
		var userDataFilePath = projectDefinition.GetUserDataFilePath();

		if (File.Exists(userDataFilePath.PathExport) == false)
		{
			return Result.Error($"No user data found at '{userDataFilePath.PathExport}'");
		}

		var allText = File.ReadAllText(userDataFilePath.PathExport);
		var userData = userDataSerializer.Deserialize(allText);

		var lastOpenedProject = userData.LastOpenedScene;
		if (lastOpenedProject == Guid.Empty)
		{
			return Result.Error("Never opened a scene before");
		}

		return assetFileWatcher
			.GetById(lastOpenedProject)
			.Then(x => x.FilePath)
			.Then(x=> x.ToAbsoluteFilePath())
			.Then(sceneFileReader.ReadSceneFile);
	}
}
