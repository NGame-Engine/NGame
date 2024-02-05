using NGameEditor.Bridge.Files;
using Singulink.IO;

namespace NGameEditor.Backend.Files;



public interface IAssetDescriptionReader
{
	AssetDescription ReadAsset(IAbsoluteFilePath absolutePath);
}



public class AssetDescriptionReader(
	IBackendAssetDeserializer backendAssetDeserializer,
	IAssetTypeDefinitionMapper assetTypeDefinitionMapper
) : IAssetDescriptionReader
{
	public AssetDescription ReadAsset(IAbsoluteFilePath absolutePath)
	{
		var readAssetResult = backendAssetDeserializer.ReadAsset(absolutePath);
		if (readAssetResult.HasError)
		{
			throw new InvalidOperationException(readAssetResult.ErrorValue!.Title);
		}


		var asset = readAssetResult.SuccessValue!;

		var assetName = absolutePath.NameWithoutExtension;

		var assetType = asset.GetType();

		return new AssetDescription(
			asset.Id,
			assetName,
			assetTypeDefinitionMapper.Map(assetType),
			absolutePath.FromAbsoluteFilePath()
		);
	}
}
