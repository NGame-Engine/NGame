using System.Text.Json;
using NGame.Assets.Common.Ecs;
using NGameEditor.Backend.Projects;
using NGameEditor.Backend.Scenes.SceneStates;
using NGameEditor.Results;
using Singulink.IO;

namespace NGameEditor.Backend.Scenes;



public interface ISceneFileReader
{
	Result<BackendScene> ReadSceneFile(IAbsoluteFilePath sceneFilePath);
}



class SceneFileReader(
	ProjectDefinition projectDefinition,
	ISceneSerializerOptionsFactory sceneSerializerOptionsFactory
)
	: ISceneFileReader
{
	public Result<BackendScene> ReadSceneFile(IAbsoluteFilePath sceneFilePath)
	{
		var allText = File.ReadAllText(sceneFilePath.PathExport);

		var componentTypes = projectDefinition.ComponentTypes;
		var options = sceneSerializerOptionsFactory.Create(componentTypes);

		var sceneAsset = JsonSerializer.Deserialize<SceneAsset>(allText, options);
		if (sceneAsset == null)
		{
			return Result.Error($"Unable to read scene {sceneFilePath.PathExport}");
		}

		var duplicateIds =
			sceneAsset
				.Entities
				.GroupBy(x => x.Id)
				.Where(x => x.Count() > 1)
				.Select(x => x.Key)
				.ToList();

		if (duplicateIds.Any())
		{
			var duplicateIdString = string.Join(", ", duplicateIds);
			return Result.Error($"Duplicate entity IDs {duplicateIdString}");
		}


		return Result.Success(
			new BackendScene(
				sceneFilePath,
				sceneAsset
			)
		);
	}
}
