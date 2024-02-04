using System.Text;
using NGame.Assets;
using NGame.Platform.Assets.Json;
using NGame.Platform.Assets.Registries;

namespace NGame.Platform.Assets.Readers;



public interface IPackedAssetDeserializer
{
	Asset Deserialize(Guid assetId);
}



public class PackedAssetDeserializer(
	IAssetSerializer assetSerializer,
	IPackedAssetStreamReader packedAssetStreamReader
)
	: IPackedAssetDeserializer
{
	public Asset Deserialize(Guid assetId)
	{
		var assetFileRaw = packedAssetStreamReader.ReadFromStream(assetId, stream =>
		{
			using var reader = new StreamReader(stream, Encoding.UTF8);
			return reader.ReadToEnd();
		});

		return assetSerializer.Deserialize(assetFileRaw);
	}
}
