using NGame.Assets;

namespace NGame.Core.Assets.Json;



public class AssetStreamReader : IAssetStreamReader
{
	private readonly AssetId _assetId;
	private readonly string? _companionFilePath;
	private readonly Func<Stream> _openStream;


	public AssetStreamReader(
		AssetId assetId,
		string? companionFilePath,
		Func<Stream> openStream
	)
	{
		_assetId = assetId;
		_companionFilePath = companionFilePath;
		_openStream = openStream;
	}


	public Stream OpenStream()
	{
		if (_companionFilePath == null)
		{
			throw new InvalidOperationException(
				$"Unable to read file for asset with ID {_assetId}"
			);
		}

		return _openStream();
	}
}
