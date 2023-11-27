namespace NGame.Assets;



public interface IAssetFromPackReader
{
	Asset ReadFromAssetPack(AssetId assetId);
}
