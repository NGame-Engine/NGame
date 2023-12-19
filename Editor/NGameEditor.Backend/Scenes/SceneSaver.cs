using System.Text.Json;
using NGame.Ecs;
using NGame.SceneAssets;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes;



public interface ISceneSaver
{
	Result SaveCurrentScene();
}



public class SceneSaver(
	ProjectDefinition projectDefinition,
	ISceneState sceneState,
	ISceneSerializerOptionsFactory sceneSerializerOptionsFactory
) : ISceneSaver
{
	public Result SaveCurrentScene()
	{
		var sceneAsset = sceneState.LoadedBackendScene.SceneAsset;

		var hasUnrecognizedComponents =
			sceneAsset
				.Entities
				.Any(x =>
					x.Components.Any(c =>
						c.GetType() == typeof(EntityComponent)
					)
				);

		if (hasUnrecognizedComponents)
		{
			return Result.Error(
				"Unrecognized components found",
				"The scene has entities with components which are not recognized." + Environment.NewLine +
				"Fix the entities marked with 🚫, or remove them from the scene before saving."
			);
		}


		var filePath = sceneState.LoadedBackendScene.FilePath;
		if (filePath == null)
		{
			return Result.Error("No scene save dialog");
		}


		var componentTypes = projectDefinition.ComponentTypes;
		var options = sceneSerializerOptionsFactory.Create(componentTypes);

		var json = JsonSerializer.Serialize(sceneAsset, options);


		FileHelper.SaveFileContentViaIntermediate(json, filePath.Path);

		return Result.Success();
	}
}
