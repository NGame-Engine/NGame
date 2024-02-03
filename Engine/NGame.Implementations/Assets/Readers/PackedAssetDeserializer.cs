using System.Text;
using NGame.Assets;
using NGame.Implementations.Assets.Json;

namespace NGame.Implementations.Assets.Readers;



public interface IPackedAssetDeserializer
{
	Asset Deserialize(AssetId assetId);
}



public class PackedAssetDeserializer : IPackedAssetDeserializer
{
	private readonly IAssetSerializer _assetSerializer;
	private readonly IPackedAssetStreamReader _packedAssetStreamReader;


	public PackedAssetDeserializer(
		IAssetSerializer assetSerializer,
		IPackedAssetStreamReader packedAssetStreamReader
	)
	{
		_assetSerializer = assetSerializer;
		_packedAssetStreamReader = packedAssetStreamReader;
	}


	public Asset Deserialize(AssetId assetId)
	{
		var assetFileRaw = _packedAssetStreamReader.ReadFromStream(assetId, stream =>
		{
			using var reader = new StreamReader(stream, Encoding.UTF8);
			return reader.ReadToEnd();
		});

		return _assetSerializer.Deserialize(assetFileRaw);
	}
}
