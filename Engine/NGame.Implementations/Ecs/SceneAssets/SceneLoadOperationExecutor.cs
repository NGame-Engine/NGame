using System.Text.Json;
using NGame.Assets;
using NGame.Ecs;
using NGame.SceneAssets;

namespace NGame.Core.Ecs.SceneAssets;

public interface ISceneLoadOperationExecutor
{
	Scene Execute(Action<float> updateProgress, AssetId sceneId);
}



public class SceneLoadOperationExecutor : ISceneLoadOperationExecutor
{
	private readonly IAssetReferenceReplacer _assetReferenceReplacer;
	private readonly IScenePopulator _scenePopulator;
	private readonly IPackedAssetStreamReader _packedAssetStreamReader;
	private readonly ISceneDeserializerOptionsFactory _optionsFactory;
	private readonly IEnumerable<ComponentTypeEntry> _componentTypes;


	public SceneLoadOperationExecutor(
		IAssetReferenceReplacer assetReferenceReplacer,
		IScenePopulator scenePopulator,
		IPackedAssetStreamReader packedAssetStreamReader, ISceneDeserializerOptionsFactory optionsFactory,
		IEnumerable<ComponentTypeEntry> componentTypes)
	{
		_assetReferenceReplacer = assetReferenceReplacer;
		_scenePopulator = scenePopulator;
		_packedAssetStreamReader = packedAssetStreamReader;
		_optionsFactory = optionsFactory;
		_componentTypes = componentTypes;
	}


	public Scene Execute(Action<float> updateProgress, AssetId sceneId)
	{
		var componentTypes = _componentTypes.Select(x => x.SubType);
		var options = _optionsFactory.Create(componentTypes);
		var sceneAsset = _packedAssetStreamReader.ReadFromStream(
			sceneId,
			stream => JsonSerializer.Deserialize<SceneAsset>(stream, options)!);

		_assetReferenceReplacer.ReplaceAssetReferences(sceneAsset);

		return _scenePopulator.Populate(sceneAsset);
	}
}
