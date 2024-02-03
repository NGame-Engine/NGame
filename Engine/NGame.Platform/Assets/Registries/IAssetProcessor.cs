using NGame.Assets;

namespace NGame.Platform.Assets.Registries;



public interface IAssetProcessor
{
	Type Type { get; }
	void Load(Asset asset, IAssetStreamReader assetStreamReader);
	void Unload(Asset asset);
}
