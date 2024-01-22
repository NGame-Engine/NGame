using NGame.Assets;
using NGameEditor.Bridge.Files;

namespace NGameEditor.Backend.Files;



public interface IAssetTypeDefinitionMapper
{
	AssetTypeDefinition Map(Type assetType);
}



public class AssetTypeDefinitionMapper : IAssetTypeDefinitionMapper
{
	public AssetTypeDefinition Map(Type assetType) =>
		new(
			AssetAttribute.GetName(assetType),
			AssetAttribute.GetDiscriminator(assetType),
			assetType != typeof(Asset)
		);
}
