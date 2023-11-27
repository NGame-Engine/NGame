using System.Text.Json;
using NGame.Assets;
using NGame.Ecs;
using NGame.Ecs.SceneAssets;
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
	private readonly ISceneAssetOptionsProvider _sceneAssetOptionsProvider;
	private readonly IPackedAssetStreamReader _packedAssetStreamReader;


	public SceneLoadOperationExecutor(
		IAssetReferenceReplacer assetReferenceReplacer,
		IScenePopulator scenePopulator,
		ISceneAssetOptionsProvider sceneAssetOptionsProvider,
		IPackedAssetStreamReader packedAssetStreamReader
	)
	{
		_assetReferenceReplacer = assetReferenceReplacer;
		_scenePopulator = scenePopulator;
		_sceneAssetOptionsProvider = sceneAssetOptionsProvider;
		_packedAssetStreamReader = packedAssetStreamReader;
	}


	public Scene Execute(Action<float> updateProgress, AssetId sceneId)
	{
		var options = _sceneAssetOptionsProvider.GetDeserializationOptions();
		var sceneAsset = _packedAssetStreamReader.ReadFromStream(
			sceneId,
			stream => JsonSerializer.Deserialize<SceneAsset>(stream, options)!);

		_assetReferenceReplacer.ReplaceAssetReferences(sceneAsset);

		return _scenePopulator.Populate(sceneAsset);
	}
}
