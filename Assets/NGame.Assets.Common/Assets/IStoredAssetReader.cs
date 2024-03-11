namespace NGame.Assets.Common.Assets;



public interface IStoredAssetReader
{
	string GetAssetJson(Guid assetId);
	byte[]? GetAssetData(Guid assetId);
}
