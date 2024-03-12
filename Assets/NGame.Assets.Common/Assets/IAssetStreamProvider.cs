namespace NGame.Assets.Common.Assets;



public interface IAssetStreamProvider
{
	bool Exists(string filePath);
	Stream Open(string filePath);
}
