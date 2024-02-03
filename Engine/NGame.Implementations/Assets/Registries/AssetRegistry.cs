using NGame.Assets;
using NGame.Implementations.Assets.Json;

namespace NGame.Implementations.Assets.Registries;



public interface IAssetRegistry
{
	void Add(Asset asset);
	void Remove(Asset asset);
	Asset? Get(AssetId assetId);
	TAsset? Get<TAsset>(AssetId assetId) where TAsset : Asset;
}



public class AssetRegistry : IAssetRegistry
{
	private readonly IAssetProcessorCollection _assetProcessorCollection;
	private readonly Dictionary<AssetId, Asset> _assets = new();


	public AssetRegistry(IAssetProcessorCollection assetProcessorCollection)
	{
		_assetProcessorCollection = assetProcessorCollection;
	}


	public void Add(Asset asset)
	{
		_assets.Add(asset.Id, asset);
		_assetProcessorCollection.Load(asset);
	}


	public void Remove(Asset asset)
	{
		_assets.Remove(asset.Id);
		_assetProcessorCollection.Unload(asset);
	}


	public Asset? Get(AssetId assetId) =>
		_assets.GetValueOrDefault(assetId);


	public TAsset? Get<TAsset>(AssetId assetId) where TAsset : Asset =>
		(TAsset?)Get(assetId);
}
