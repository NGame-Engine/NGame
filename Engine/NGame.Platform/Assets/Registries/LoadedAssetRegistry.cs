using NGame.Assets;

namespace NGame.Platform.Assets.Registries;



public interface ILoadedAssetRegistry<T>
{
	void Add(Asset asset, T ownFormat);
	T Get(Asset asset);
	IEnumerable<Asset> GetAllRegisteredAssets();
	void Remove(Asset asset);
}



public class LoadedAssetRegistry<T> : ILoadedAssetRegistry<T>
{
	private readonly Dictionary<Asset, T> _assets = new();


	public void Add(Asset asset, T ownFormat)
	{
		_assets.Add(asset, ownFormat);
	}


	public T Get(Asset asset) => _assets[asset];


	public IEnumerable<Asset> GetAllRegisteredAssets() => _assets.Keys.ToList();


	public void Remove(Asset asset) => _assets.Remove(asset);
}
