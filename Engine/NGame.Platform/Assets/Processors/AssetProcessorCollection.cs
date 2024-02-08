using NGame.Assets;

namespace NGame.Platform.Assets.Processors;



public interface IAssetProcessorCollection
{
	void Load(Asset asset, byte[]? companionFileBytes);
	void Unload(Asset asset);
}



public class AssetProcessorCollection(
	IEnumerable<IAssetProcessor> assetProcessors
)
	: IAssetProcessorCollection
{
	public void Load(Asset asset, byte[]? companionFileBytes)
	{
		var type = asset.GetType();
		var processors = assetProcessors.Where(x => x.Type == type);

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Load(asset, companionFileBytes);
		}
	}


	public void Unload(Asset asset)
	{
		var type = asset.GetType();
		var processors = assetProcessors.Where(x => x.Type == type);

		foreach (var assetProcessor in processors)
		{
			assetProcessor.Unload(asset);
		}
	}
}
