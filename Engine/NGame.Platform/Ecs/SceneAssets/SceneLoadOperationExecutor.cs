using System.Text.Json;
using NGame.Assets.Common.Ecs;
using NGame.Ecs;
using NGame.Platform.Assets.Unpacking;

namespace NGame.Platform.Ecs.SceneAssets;



public interface ISceneLoadOperationExecutor
{
	// ReSharper disable once UnusedParameter.Global
	Scene Execute(Action<float> updateProgress, Guid sceneId);
}



public class SceneLoadOperationExecutor(
	IAssetReferenceReplacer assetReferenceReplacer,
	IScenePopulator scenePopulator,
	ISceneSerializerOptionsFactory optionsFactory,
	IEnumerable<ComponentTypeEntry> types,
	IAssetUnpacker assetUnpacker
)
	: ISceneLoadOperationExecutor
{
	public Scene Execute(Action<float> updateProgress, Guid sceneId)
	{
		var componentTypes = types.Select(x => x.SubType);
		var options = optionsFactory.Create(componentTypes);

		var rawAsset = assetUnpacker.Unpack(sceneId);
		var sceneAsset = JsonSerializer.Deserialize<SceneAsset>(rawAsset.Json, options)!;


		assetReferenceReplacer.ReplaceAssetReferences(sceneAsset);

		return scenePopulator.Populate(sceneAsset);
	}
}
