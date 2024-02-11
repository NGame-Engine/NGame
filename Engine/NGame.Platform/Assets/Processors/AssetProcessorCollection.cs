using NGame.Assets;
using NGame.Platform.Assets.Unpacking;
using NGame.Platform.Ecs.SceneAssets;

namespace NGame.Platform.Assets.Processors;



public interface IAssetProcessorCollection
{
	void LoadAssets(IEnumerable<AssetReference> assetReferences);
	void Load(Asset asset, byte[]? companionFileBytes);
	void Unload(Asset asset);
}



public class AssetProcessorCollection(
	IEnumerable<IAssetProcessor> assetProcessors,
	IAssetUnpacker assetUnpacker
)
	: IAssetProcessorCollection
{
	public void LoadAssets(IEnumerable<AssetReference> assetReferences)
	{
		var processedAssetIds = new HashSet<Guid>();

		var orderedAssetReferences =
			assetReferences
				.OrderByDescending(x => x.ReferenceLevel);

		foreach (var assetReference in orderedAssetReferences)
		{
			var assetId = assetReference.Asset.Id;
			if (processedAssetIds.Add(assetId) == false) continue;

			var asset = assetReference.Asset;
			var companionFileBytes = assetUnpacker.GetCompanionFileBytes(assetId);

			var type = asset.GetType();
			var processors = assetProcessors.Where(x => x.Type == type);

			foreach (var assetProcessor in processors)
			{
				assetProcessor.Load(asset, companionFileBytes);
			}
		}
	}


	public void Load(Asset asset, byte[]? companionFileBytes)
	{
		var type = asset.GetType();
		var processors = assetProcessors.Where(x => x.Type == type);

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Load(asset, companionFileBytes);
		}
	}


	public void Unload(Asset asset)
	{
		var type = asset.GetType();
		var processors = assetProcessors.Where(x => x.Type == type);

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Unload(asset);
		}
	}
}
