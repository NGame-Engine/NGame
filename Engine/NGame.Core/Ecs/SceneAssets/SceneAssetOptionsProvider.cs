using System.Text.Json;
using NGame.Assets.Implementations;
using NGame.Ecs;
using NGame.Ecs.SceneAssets;
using NGame.SceneAssets;

namespace NGame.Core.Ecs.SceneAssets;



public class SceneAssetOptionsProvider : ISceneAssetOptionsProvider
{
	private readonly IEnumerable<ComponentTypeEntry> _componentTypes;
	private readonly ISceneDeserializerOptionsFactory _sceneDeserializerOptionsFactory;


	public SceneAssetOptionsProvider(
		IEnumerable<ComponentTypeEntry> componentTypes,
		ISceneDeserializerOptionsFactory sceneDeserializerOptionsFactory
	)
	{
		_componentTypes = componentTypes;
		_sceneDeserializerOptionsFactory = sceneDeserializerOptionsFactory;
	}


	private JsonSerializerOptions? JsonSerializerOptions { get; set; }


	public JsonSerializerOptions GetDeserializationOptions() =>
		JsonSerializerOptions ??= CreateSerializerOptions();


	private JsonSerializerOptions CreateSerializerOptions()
	{
		var componentTypes =
			_componentTypes
				.Select(x => x.SubType);

		return _sceneDeserializerOptionsFactory.Create(componentTypes);
	}
}
