using NGame.Assets;

namespace NGame.Platform.Assets.Registries;



public interface IPackedAssetStreamReader
{
	T ReadFromStream<T>(AssetId assetId, Func<Stream, T> useStream);
}
