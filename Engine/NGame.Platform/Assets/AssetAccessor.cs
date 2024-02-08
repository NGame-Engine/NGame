using NGame.Assets;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Processors;
using NGame.Platform.Assets.Registries;
using NGame.Platform.Assets.Unpacking;

namespace NGame.Platform.Assets;



public class AssetAccessor(
	IAssetProcessorCollection assetProcessorCollection,
	IAssetRegistry assetRegistry,
	IAssetSerializer assetSerializer,
	IAssetUnpacker assetUnpacker
)
	: IAssetAccessor
{
	public Asset ReadFromAssetPack(Guid assetId)
	{
		var asset = assetRegistry.Get(assetId);
		if (asset != null) return asset;

		var rawAsset = assetUnpacker.Unpack(assetId);
		asset = assetSerializer.Deserialize(rawAsset.Json);
		assetProcessorCollection.Load(asset, rawAsset.fileBytes);
		assetRegistry.Add(asset);

		return asset;
	}
}
