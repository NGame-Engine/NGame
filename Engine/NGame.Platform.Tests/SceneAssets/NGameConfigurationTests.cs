using System.Text.Json;
using NGame.Tooling.Assets;
using NGame.Tooling.Ecs;

namespace NGame.Implementations.Tests.SceneAssets;



public class NGameConfigurationTests
{
	[Fact]
	public void NGameConfiguration_ExampleJson_CanBeDeserialized()
	{
		// Arrange
		const string json = """
		                    {
		                      "Scenes": [
		                        "BEF0E660-8BEB-4124-AC58-477307B54CCE",
		                        "C505053A-2998-4D73-B7D2-8633F0F90754"
		                      ],
		                      "StartScene": "BEF0E660-8BEB-4124-AC58-477307B54CCE"
		                    }
		                    """;

		var jsonSerializerOptions = new JsonSerializerOptions
		{
			Converters =
			{
				new AssetIdConverter(),
				new SemVersionConverter()
			}
		};

		// Act
		var nGameConfiguration = JsonSerializer
			.Deserialize<SceneAssetsConfiguration>(json, jsonSerializerOptions)!;


		// Assert
		Assert.NotNull(nGameConfiguration.Scenes);
		Assert.Equal(2, nGameConfiguration.Scenes.Count);
		Assert.Equal(Guid.Parse("BEF0E660-8BEB-4124-AC58-477307B54CCE"), nGameConfiguration.StartScene);
	}
}
