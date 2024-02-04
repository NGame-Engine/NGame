namespace NGame.Platform.Assets.Registries;



public interface IPackedAssetStreamReader
{
	T ReadFromStream<T>(Guid assetId, Func<Stream, T> useStream);
}
