namespace NGame.Assets;



public interface IAssetProcessor
{
	Type Type { get; }
	void Load(Asset asset, IAssetStreamReader assetStreamReader);
	void Unload(Asset asset);
}



public interface IAssetStreamReader
{
	Stream OpenStream();
}
