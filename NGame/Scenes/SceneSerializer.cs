using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using NGame.Assets;
using NGame.Ecs;

namespace NGame.Scenes;

public interface ISceneSerializationOptionsProvider
{
	JsonSerializerOptions CreateJsonSerializerOptions();
}



internal class SceneSerializer : IAssetSerializer<Scene>, ISceneSerializationOptionsProvider
{
	private readonly IComponentTypeRegistry _componentTypeRegistry;
	private readonly ILogger<SceneSerializer> _logger;


	public SceneSerializer(IComponentTypeRegistry componentTypeRegistry, ILogger<SceneSerializer> logger)
	{
		_componentTypeRegistry = componentTypeRegistry;
		_logger = logger;
	}


	public Scene Deserialize(string filePath)
	{
		using var fileStream = File.OpenRead(filePath);
		var jsonSerializerOptions = CreateJsonSerializerOptions();
		return JsonSerializer.Deserialize<Scene>(fileStream, jsonSerializerOptions)!;
	}


	public JsonSerializerOptions CreateJsonSerializerOptions()
	{
		var types = _componentTypeRegistry.GetComponentTypes();

		var typesString = string.Join(Environment.NewLine, types.Select(x => x.Name));
		_logger.LogInformation("Including components {0}{1}", Environment.NewLine, typesString);

		return new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			TypeInfoResolver =
				new PolymorphicTypeResolver(
					new Dictionary<Type, JsonPolymorphismOptions>
					{
						[typeof(IComponent)] = PolymorphismOptions.ForComponents(types)
					}
				)
		};
	}
}
