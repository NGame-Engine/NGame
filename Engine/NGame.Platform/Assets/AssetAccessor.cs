using NGame.Assets;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Registries;
using NGame.Platform.Assets.Unpacking;

namespace NGame.Platform.Assets;



public interface IAssetAccessor
{
	Asset ReadFromAssetPack(Guid assetId);
}



public class AssetAccessor(
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

		var assetJsonContent = assetUnpacker.GetAssetJsonContent(assetId);
		asset = assetSerializer.Deserialize(assetJsonContent);
		assetRegistry.Add(asset);

		return asset;
	}
}
