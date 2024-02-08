using NGame.Assets;

namespace NGame.Platform.Assets.Registries;



public interface IAssetProcessor
{
	Type Type { get; }
	void Load(Asset asset, byte[]? companionFileBytes);
	void Unload(Asset asset);
}
