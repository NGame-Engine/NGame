namespace NGame.Assets.Common.Assets;



public class UnpackedAsset(string json, byte[]? data)
{
	public string Json { get; } = json;
	public byte[]? Data { get; } = data;
}



public interface IAssetProvider
{
	UnpackedAsset GetAsset(Guid assetId);
}
