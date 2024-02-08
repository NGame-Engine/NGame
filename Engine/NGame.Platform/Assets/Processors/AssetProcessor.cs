using NGame.Assets;

namespace NGame.Platform.Assets.Processors;



public interface IAssetProcessor
{
	Type Type { get; }
	void Load(Asset asset, byte[]? companionFileBytes);
	void Unload(Asset asset);
}



public abstract class AssetProcessor<TAsset>
	: IAssetProcessor where TAsset : Asset
{
	public Type Type => typeof(TAsset);

	protected abstract void Load(TAsset asset, byte[]? companionFileBytes);
	protected abstract void Unload(TAsset asset);


	void IAssetProcessor.Load(Asset asset, byte[]? companionFileBytes) =>
		Load((TAsset)asset, companionFileBytes);


	void IAssetProcessor.Unload(Asset asset) =>
		Unload((TAsset)asset);
}
