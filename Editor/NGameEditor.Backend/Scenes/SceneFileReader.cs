using System.Text.Json;
using NGame.SceneAssets;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes;



public interface ISceneFileReader
{
	Result<BackendScene> ReadSceneFile(AbsolutePath sceneFilePath);
}



class SceneFileReader(
	ProjectDefinition projectDefinition,
	ISceneSerializerOptionsFactory sceneSerializerOptionsFactory
)
	: ISceneFileReader
{
	public Result<BackendScene> ReadSceneFile(AbsolutePath sceneFilePath)
	{
		var allText = File.ReadAllText(sceneFilePath.Path);

		var componentTypes = projectDefinition.ComponentTypes;
		var options = sceneSerializerOptionsFactory.Create(componentTypes);

		var sceneAsset = JsonSerializer.Deserialize<SceneAsset>(allText, options);


		return sceneAsset == null
			? Result.Error($"Unable to read scene {sceneFilePath.Path}")
			: Result.Success(
				new BackendScene(
					sceneFilePath,
					sceneAsset
				)
			);
	}
}
