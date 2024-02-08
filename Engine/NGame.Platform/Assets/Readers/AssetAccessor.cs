using NGame.Assets;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets.Readers;



public class AssetAccessor(
	IAssetProcessorCollection assetProcessorCollection,
	IAssetRegistry assetRegistry
)
	: IAssetAccessor
{
	public Asset ReadFromAssetPack(Guid assetId)
	{
		var asset = assetRegistry.Get(assetId);
		if (asset != null) return asset;

		asset = assetProcessorCollection.Load(assetId);
		assetProcessorCollection.Load(asset);
		assetRegistry.Add(asset);

		return asset;
	}
}
