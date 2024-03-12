using NGame.Assets;
using NGame.Assets.Common.Assets;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets;



public interface IAssetAccessor
{
	Asset ReadFromAssetPack(Guid assetId);
}



public class AssetAccessor(
	IAssetRegistry assetRegistry,
	IAssetSerializer assetSerializer,
	IStoredAssetReader storedAssetReader
)
	: IAssetAccessor
{
	public Asset ReadFromAssetPack(Guid assetId)
	{
		var asset = assetRegistry.Get(assetId);
		if (asset != null) return asset;

		var assetJsonContent = storedAssetReader.GetAssetJson(assetId);
		asset = assetSerializer.Deserialize(assetJsonContent);
		assetRegistry.Add(asset);

		return asset;
	}
}
