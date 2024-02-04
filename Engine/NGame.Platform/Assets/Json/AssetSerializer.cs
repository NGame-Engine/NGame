using System.Text.Json;
using NGame.Assets;
using NGame.Assets.Common.Assets;

namespace NGame.Platform.Assets.Json;



public interface IAssetSerializer
{
	Asset Deserialize(Stream stream);
	Asset Deserialize(string json);
}



public class AssetSerializer(
	IEnumerable<AssetTypeEntry> types,
	IAssetDeserializerOptionsFactory assetDeserializerOptionsFactory
)
	: IAssetSerializer
{
	private JsonSerializerOptions? JsonSerializerOptions { get; set; }


	public Asset Deserialize(Stream stream)
	{
		JsonSerializerOptions ??= CreateSerializerOptions();

		try
		{
			return JsonSerializer.Deserialize<Asset>(stream, JsonSerializerOptions)!;
		}
		catch (NotSupportedException e)
		{
			if (!e.Message.StartsWith("Deserialization of types without a parameterless constructor"))
			{
				throw;
			}

			var message = $"Unable to find type for asset in {stream}";
			throw new InvalidOperationException(message);
		}
	}


	public Asset Deserialize(string json)
	{
		JsonSerializerOptions ??= CreateSerializerOptions();
		try
		{
			return JsonSerializer.Deserialize<Asset>(json, JsonSerializerOptions)!;
		}
		catch (NotSupportedException e)
		{
			if (!e.Message.StartsWith("Deserialization of types without a parameterless constructor"))
			{
				throw;
			}

			var message = $"Unable to find correct subtype for asset, did you register it? JSON: {json}";
			throw new InvalidOperationException(message);
		}
	}


	private JsonSerializerOptions CreateSerializerOptions()
	{
		var assetTypes =
			types
				.Select(x => x.SubType);

		return assetDeserializerOptionsFactory.Create(assetTypes);
	}
}
