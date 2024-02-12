using System.Text.Json;
using NGame.Assets;
using NGame.Assets.Common.Assets;

namespace NGame.Platform.Assets.Json;



public interface IAssetSerializer
{
	Asset Deserialize(string json);
}



public class AssetSerializer(
	IEnumerable<AssetTypeEntry> types,
	IAssetDeserializerOptionsFactory assetDeserializerOptionsFactory
)
	: IAssetSerializer
{
	private JsonSerializerOptions? JsonSerializerOptions { get; set; }


	public Asset Deserialize(string json)
	{
		JsonSerializerOptions ??= CreateSerializerOptions();
		try
		{
			return JsonSerializer.Deserialize<Asset>(json, JsonSerializerOptions)!;
		}
		catch (Exception e)
		{
			if (e is NotSupportedException &&
			    e.Message.StartsWith("Deserialization of types without a parameterless constructor"))
			{
				var message = $"Unable to find correct subtype for asset, did you register it? JSON: {json}";
				throw new InvalidOperationException(message);
			}

			throw new InvalidOperationException(
				$"Unable to deserialize JSON {json}",
				e
			);
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
