namespace NGame.Assets.Common.Assets;



public interface IAssetStreamProvider
{
	bool Exists(string fileName);
	Stream Open(string fileName);
}
