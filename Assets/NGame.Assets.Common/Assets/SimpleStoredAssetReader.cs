using System.Text;

namespace NGame.Assets.Common.Assets;



public class SimpleStoredAssetReader(
	IAssetStreamProvider assetStreamProvider
) : IStoredAssetReader
{
	public string GetAssetJson(Guid assetId)
	{
		var path = $"{assetId}/asset.json";
		using var stream = assetStreamProvider.Open(path);
		using var reader = new StreamReader(stream, Encoding.UTF8);
		return reader.ReadToEnd();
	}


	public byte[]? GetAssetData(Guid assetId)
	{
		var path = $"{assetId}/data.bin";
		if (assetStreamProvider.Exists(path) == false) return null;

		using var stream = assetStreamProvider.Open(path);
		using var memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		return memoryStream.ToArray();
	}
}
