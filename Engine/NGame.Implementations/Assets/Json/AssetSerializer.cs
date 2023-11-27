using System.Text.Json;
using NGame.Assets;

namespace NGame.Core.Assets.Json;



public interface IAssetSerializer
{
	Asset Deserialize(Stream stream);
	Asset Deserialize(string json);
}



public class AssetSerializer : IAssetSerializer
{
	private readonly IEnumerable<AssetTypeEntry> _assetTypes;
	private readonly IAssetDeserializerOptionsFactory _assetDeserializerOptionsFactory;


	public AssetSerializer(
		IEnumerable<AssetTypeEntry> assetTypes,
		IAssetDeserializerOptionsFactory assetDeserializerOptionsFactory
	)
	{
		_assetTypes = assetTypes;
		_assetDeserializerOptionsFactory = assetDeserializerOptionsFactory;
	}


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
			_assetTypes
				.Select(x => x.SubType);

		return _assetDeserializerOptionsFactory.Create(assetTypes);
	}
}
