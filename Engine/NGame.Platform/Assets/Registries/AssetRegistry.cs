using NGame.Assets;

namespace NGame.Platform.Assets.Registries;



public interface IAssetRegistry
{
	void Add(Asset asset);
	void Remove(Asset asset);
	Asset? Get(Guid assetId);
	TAsset? Get<TAsset>(Guid assetId) where TAsset : Asset;
}



public class AssetRegistry : IAssetRegistry
{
	private readonly Dictionary<Guid, Asset> _assets = new();


	public void Add(Asset asset)
	{
		_assets.Add(asset.Id, asset);
	}


	public void Remove(Asset asset)
	{
		_assets.Remove(asset.Id);
	}


	public Asset? Get(Guid assetId) =>
		_assets.GetValueOrDefault(assetId);


	public TAsset? Get<TAsset>(Guid assetId) where TAsset : Asset =>
		(TAsset?)Get(assetId);
}
