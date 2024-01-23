using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Files;



public interface IAssetDescriptionReader
{
	AssetDescription ReadAsset(AbsolutePath absolutePath);
}



public class AssetDescriptionReader(
	IBackendAssetDeserializer backendAssetDeserializer,
	IAssetTypeDefinitionMapper assetTypeDefinitionMapper
) : IAssetDescriptionReader
{
	public AssetDescription ReadAsset(AbsolutePath absolutePath)
	{
		var readAssetResult = backendAssetDeserializer.ReadAsset(absolutePath);
		if (readAssetResult.HasError)
		{
			throw new InvalidOperationException(readAssetResult.ErrorValue!.Title);
		}

		
		var asset = readAssetResult.SuccessValue!;

		var assetName = Path.GetFileNameWithoutExtension(absolutePath.Path);
		
		var assetType = asset.GetType();

		return new AssetDescription(
			asset.Id.Id,
			assetName,
			assetTypeDefinitionMapper.Map(assetType),
			absolutePath,
			[]
		);
	}
}
