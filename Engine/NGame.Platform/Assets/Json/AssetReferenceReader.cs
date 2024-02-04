using NGame.Assets;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets.Json;



public class AssetStreamReader(
	AssetId assetId,
	string? companionFilePath,
	Func<Stream> openStream
)
	: IAssetStreamReader
{
	public Stream OpenStream()
	{
		if (companionFilePath == null)
		{
			throw new InvalidOperationException(
				$"Unable to read file for asset with ID {assetId}"
			);
		}

		return openStream();
	}
}
