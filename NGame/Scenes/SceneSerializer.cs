using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using NGame.Assets;
using NGame.Ecs;

namespace NGame.Scenes;

public interface ISceneSerializationOptionsProvider
{
	JsonSerializerOptions CreateJsonSerializerOptions();
}



public class SceneSerializer : IAssetSerializer<Scene>, ISceneSerializationOptionsProvider
{
	private readonly IComponentTypeRegistry _componentTypeRegistry;


	public SceneSerializer(IComponentTypeRegistry componentTypeRegistry)
	{
		_componentTypeRegistry = componentTypeRegistry;
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

		return new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			TypeInfoResolver =
				new PolymorphicTypeResolver(
					new Dictionary<Type, JsonPolymorphismOptions>
					{
						[typeof(Component)] = PolymorphismOptions.ForComponents(types)
					}
				)
		};
	}
}
