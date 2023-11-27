using NGame.Assets;
using NGame.Core.Assets.Registries;

namespace NGame.Core.Assets.Readers;



public class AssetFromPackReader : IAssetFromPackReader
{
	private readonly IPackedAssetDeserializer _packedAssetDeserializer;
	private readonly IAssetRegistry _assetRegistry;


	public AssetFromPackReader(IPackedAssetDeserializer packedAssetDeserializer, IAssetRegistry assetRegistry)
	{
		_packedAssetDeserializer = packedAssetDeserializer;
		_assetRegistry = assetRegistry;
	}


	public Asset ReadFromAssetPack(AssetId assetId)
	{
		var asset = _assetRegistry.Get(assetId);
		if (asset != null) return asset;

		asset = _packedAssetDeserializer.Deserialize(assetId);
		_assetRegistry.Add(asset);

		return asset;
	}
}
