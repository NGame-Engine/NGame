using System.Text.Json;
using NGame.Assets;
using NGameEditor.Backend.Projects;
using NGameEditor.Bridge.Shared;
using NGameEditor.Results;

namespace NGameEditor.Backend.Files;



public interface IBackendAssetDeserializer
{
	Result<Asset> ReadAsset(AbsolutePath absolutePath);
}



public class BackendAssetDeserializer(
	IAssetDeserializerOptionsFactory assetDeserializerOptionsFactory,
	ProjectDefinition projectDefinition
) : IBackendAssetDeserializer
{
	private JsonSerializerOptions? Options { get; set; }


	public Result<Asset> ReadAsset(AbsolutePath absolutePath)
	{
		Options ??= CreateJsonSerializerOptions();


		var allText = File.ReadAllText(absolutePath.Path);

		try
		{
			var asset = JsonSerializer.Deserialize<Asset>(allText, Options)!;
			return Result.Success(asset);
		}
		catch (NotSupportedException e)
		{
			var assetTypeIsNotRecognized = e.Message.StartsWith(
				"Deserialization of types without a parameterless constructor",
				StringComparison.OrdinalIgnoreCase
			);

			if (assetTypeIsNotRecognized)
			{
				return Result.Error($"Unknown type of asset at {absolutePath.Path}");
			}

			throw;
		}
	}


	private JsonSerializerOptions CreateJsonSerializerOptions()
	{
		var assetTypes = projectDefinition.AssetTypes;
		var options = assetDeserializerOptionsFactory.Create(assetTypes);
		options.WriteIndented = true;
		return options;
	}
}
