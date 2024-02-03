namespace NGame.Assets;



public interface IAssetAccessor
{
	Asset ReadFromAssetPack(AssetId assetId);
}
