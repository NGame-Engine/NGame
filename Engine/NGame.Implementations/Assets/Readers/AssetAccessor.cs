using NGame.Assets;
using NGame.Implementations.Assets.Registries;

namespace NGame.Implementations.Assets.Readers;



public class AssetAccessor(
	IPackedAssetDeserializer packedAssetDeserializer,
	IAssetRegistry assetRegistry
)
	: IAssetAccessor
{
	public Asset ReadFromAssetPack(AssetId assetId)
	{
		var asset = assetRegistry.Get(assetId);
		if (asset != null) return asset;

		asset = packedAssetDeserializer.Deserialize(assetId);
		assetRegistry.Add(asset);

		return asset;
	}
}
