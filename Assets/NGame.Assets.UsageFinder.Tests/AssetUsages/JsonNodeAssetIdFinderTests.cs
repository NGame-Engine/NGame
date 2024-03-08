using System.Text.Json.Nodes;
using FluentAssertions;
using NGame.Assets.UsageFinder.AssetUsages;

namespace NGame.Assets.UsageFinder.Tests.AssetUsages;



public class JsonNodeAssetIdFinderTests
{
	private static IJsonNodeAssetIdFinder Create() => new JsonNodeAssetIdFinder();

	[Fact]
	public void FindReferencedIdsRecursively_SceneAsset_FindsAllIds()
	{
		// Arrange
		var input = @"{
  ""$asset"": ""NGame.SceneAsset"",
  ""Id"": ""0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC"",
  ""SerializerVersion"": ""0.0.1"",
  ""Entities"": [
    {
      ""Id"": ""0F85A235-5A85-4BFB-8BCB-2B7CAF7BF9CC"",
      ""Name"": ""First Entity"",
      ""Components"": [
        {
          ""$type"": ""NGame.Camera2D"",
          ""Id"": ""2D4E546E-C3CA-48AC-81F7-D2B1B0B8AA6C"",
          ""Zoom"": 0.01
        },
        {
          ""$type"": ""CameraMover"",
          ""Id"": ""B44D3A9F-8E52-49C2-8394-519A487CC6D5"",
          ""Speed"": 0.1
        }
      ]
    },
    {
      ""Id"": ""251D97B8-433B-4618-BD06-72898C91D86F"",
      ""Name"": ""Second Entity"",
      ""Components"": [
        {
          ""$type"": ""NGame.LineRenderer2D"",
          ""Id"": ""39E96192-5447-4C11-BF48-24319A0882D0"",
          ""Line"": {
            ""Vertices"": [
              {
                ""X"": 0,
                ""Y"": 0
              },
              {
                ""X"": 0,
                ""Y"": 1
              },
              {
                ""X"": 1,
                ""Y"": 1
              }
            ]
          }
        }
      ]
    },
    {
      ""Id"": ""251D97B8-433B-4618-BD06-72898C91D86F"",
      ""Name"": ""Third Entity"",
      ""Components"": [
        {
          ""$asset"": ""NGame.SpriteRenderer2D"",
          ""Id"": ""E58E6A19-8FE8-410A-B2E2-8CF08826A09B"",
          ""Sprite"": {
            ""Texture2D"": {
              ""$type"": ""NGame.Texture2D"",
              ""Id"": ""82D5C696-A2DC-4E4E-ADBE-03D024332150"",
              ""SerializerVersion"": ""0.0.1""
            },
            ""SourceRectangle"": {
              ""X"": 0,
              ""Y"": 0,
              ""Width"": 256,
              ""Height"": 256
            }
          }
        }
      ]
    }
  ]
}
";

		var jsonNodeAssetIdFinder = Create();
		var jsonNode = JsonNode.Parse(input);


		// Act
		var result = jsonNodeAssetIdFinder.FindReferencedIdsRecursively(jsonNode);


		// Assert
		var referencedIds = result.ToHashSet();
		referencedIds.Should().Contain(Guid.Parse("0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC"));
		referencedIds.Should().Contain(Guid.Parse("E58E6A19-8FE8-410A-B2E2-8CF08826A09B"));
	}
}
