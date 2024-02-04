using System.Text.Json;
using NGame.Assets.Common.Ecs;
using NGame.Ecs;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Ecs.SceneAssets;

public interface ISceneLoadOperationExecutor
{
	// ReSharper disable once UnusedParameter.Global
	Scene Execute(Action<float> updateProgress, Guid sceneId);
}



public class SceneLoadOperationExecutor(
	IAssetReferenceReplacer assetReferenceReplacer,
	IScenePopulator scenePopulator,
	IPackedAssetStreamReader packedAssetStreamReader,
	ISceneSerializerOptionsFactory optionsFactory,
	IEnumerable<ComponentTypeEntry> types
)
	: ISceneLoadOperationExecutor
{
	public Scene Execute(Action<float> updateProgress, Guid sceneId)
	{
		var componentTypes = types.Select(x => x.SubType);
		var options = optionsFactory.Create(componentTypes);
		var sceneAsset = packedAssetStreamReader.ReadFromStream(
			sceneId,
			stream => JsonSerializer.Deserialize<SceneAsset>(stream, options)!);

		assetReferenceReplacer.ReplaceAssetReferences(sceneAsset);

		return scenePopulator.Populate(sceneAsset);
	}
}
