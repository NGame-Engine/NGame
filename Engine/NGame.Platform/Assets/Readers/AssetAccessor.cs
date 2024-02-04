using NGame.Assets;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets.Readers;



public class AssetAccessor(
	IPackedAssetDeserializer packedAssetDeserializer,
	IAssetRegistry assetRegistry
)
	: IAssetAccessor
{
	public Asset ReadFromAssetPack(Guid assetId)
	{
		var asset = assetRegistry.Get(assetId);
		if (asset != null) return asset;

		asset = packedAssetDeserializer.Deserialize(assetId);
		assetRegistry.Add(asset);

		return asset;
	}
}
