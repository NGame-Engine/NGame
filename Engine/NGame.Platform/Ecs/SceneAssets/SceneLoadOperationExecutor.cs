using System.Text.Json;
using NGame.Assets.Common.Ecs;
using NGame.Ecs;
using NGame.Platform.Assets.Processors;
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
	IAssetUnpacker assetUnpacker,
	IAssetProcessorCollection assetProcessorCollection
)
	: ISceneLoadOperationExecutor
{
	public Scene Execute(Action<float> updateProgress, Guid sceneId)
	{
		var componentTypes = types.Select(x => x.SubType);
		var options = optionsFactory.Create(componentTypes);
		//sceneId = Guid.Parse("0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC"); // TODO load real ID on android
		var assetJsonContent = assetUnpacker.GetAssetJsonContent(sceneId);
		var sceneAsset = JsonSerializer.Deserialize<SceneAsset>(assetJsonContent, options)!;

		var replacedAssets = assetReferenceReplacer.ReplaceAssetReferences(sceneAsset);
		assetProcessorCollection.LoadAssets(replacedAssets);

		return scenePopulator.Populate(sceneAsset);
	}
}
