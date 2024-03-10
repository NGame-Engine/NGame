namespace NGame.Assets.Common.Assets;



public interface IAssetStreamProvider
{
	Stream Open(string fileName);
}
