using NGame.Assets;

namespace NGame.Platform.Assets;



public interface IAssetAccessor
{
	Asset ReadFromAssetPack(Guid assetId);
}
