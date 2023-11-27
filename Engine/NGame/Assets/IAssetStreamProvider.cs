namespace NGame.Assets;



public interface IAssetStreamProvider
{
	Stream Open(string fileName);
}
