using System.Text.Json;
using System.Text.Json.Serialization;
using NGame.Assets;

namespace NGame.Cli.PackAssets.Writers;



public class BasicAsset : Asset;



public interface IBasicAssetReader
{
	BasicAsset ReadFile(string filePath);
}



public class BasicAssetReader(IEnumerable<JsonConverter> jsonConverters) : IBasicAssetReader
{
	public BasicAsset ReadFile(string filePath)
	{
		var jsonSerializerOptions = new JsonSerializerOptions();
		foreach (var jsonConverter in jsonConverters)
		{
			jsonSerializerOptions.Converters.Add(jsonConverter);
		}

		try
		{
			using var fileStream = File.OpenRead(filePath);
			var typeIdAsset = JsonSerializer.Deserialize<BasicAsset>(fileStream, jsonSerializerOptions)!;
			if (typeIdAsset == null)
			{
				throw new InvalidOperationException($"Asset at {fileStream} is null");
			}

			return typeIdAsset;
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Unable to read asset at '{filePath}'", e);
		}
	}
}
