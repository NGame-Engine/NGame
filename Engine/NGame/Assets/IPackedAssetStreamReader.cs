namespace NGame.Assets;



public interface IPackedAssetStreamReader
{
	T ReadFromStream<T>(AssetId assetId, Func<Stream, T> useStream);
}
