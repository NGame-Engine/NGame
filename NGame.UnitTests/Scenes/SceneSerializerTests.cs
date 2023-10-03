using NGame.Assets;
using NGame.Ecs;

namespace NGame.UnitTests.Scenes;

public class SceneSerializerTests
{
	[Component(StableDiscriminator = "TestComponent")]
	private class TestComponent : Component
	{
		public int TestValue { get; set; }
	}



	[Fact]
	public void Do()
	{
		/*// Arrange
		var input = """
		            {
		              "$type": "March.Scenes.SceneAsset",
		              "id": "0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC",
		              "serializerVersion": "0.0.1",
		              "entities": [
		                {
		                  "id": "0F85A235-5A85-4BFB-8BCB-2B7CAF7BF9CC",
		                  "name": "First Entity",
		                  "components": [
		                    {
		                      "$type": "TestComponent",
		                      "id": "0F85A235-5A85-4BFB-8BCB-2B8CAF7BF9CC",
		                      "testValue": 4
		                    }
		                  ]
		                }
		              ]
		            }
		            """;

		var componentTypeRegistry = Substitute.For<IComponentTypeRegistry>();
		componentTypeRegistry
			.GetComponentTypes()
			.Returns(new[] { typeof(TestComponent) });

		var logger = Substitute.For<ILogger<SceneSerializer>>();

		var sceneSerializer = new SceneSerializer(componentTypeRegistry, logger);


		// Act
		var jsonSerializerOptions = sceneSerializer.CreateJsonSerializerOptions();
		var scene = JsonSerializer.Deserialize<Scene>(input, jsonSerializerOptions)!;


		// Assert
		Assert.NotNull(scene);

		var entities = scene.Entities;
		Assert.NotNull(entities);
		Assert.Equal(1, entities.Count);

		var entity = entities.First();
		var components = entity.GetComponents();
		Assert.NotNull(components);
		Assert.Equal(1, components.Count);

		var component = components.First();
		Assert.IsType<TestComponent>(component);

		var testComponent = (TestComponent)component;
		Assert.Equal(4, testComponent.TestValue);*/
	}
}
