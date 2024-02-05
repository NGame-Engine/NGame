using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets.Common.Ecs;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Scenes;



internal interface ISceneFileIdReader
{
	Result<Guid> GetId(CompatibleAbsolutePath sceneFilePath);
}



class SceneFileIdReader : ISceneFileIdReader
{
	private readonly IEnumerable<JsonConverter> _jsonConverters;


	public SceneFileIdReader(IEnumerable<JsonConverter> jsonConverters)
	{
		_jsonConverters = jsonConverters;
	}


	public Result<Guid> GetId(CompatibleAbsolutePath sceneFilePath)
	{
		var allText = File.ReadAllText(sceneFilePath.Path);

		var options = new JsonSerializerOptions();
		foreach (var jsonConverter in _jsonConverters)
		{
			options.Converters.Add(jsonConverter);
		}

		var sceneAsset = JsonSerializer.Deserialize<SceneAsset>(allText, options);

		if (sceneAsset == null)
		{
			return Result.Error($"Unable to read scene {sceneFilePath.Path}");
		}


		var id = sceneAsset.Id;
		return Result.Success(id);
	}
}
