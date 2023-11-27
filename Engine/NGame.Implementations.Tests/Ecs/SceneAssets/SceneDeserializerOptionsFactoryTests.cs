using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using NGame.Assets.Implementations;
using NGame.Ecs;
using NGame.Ecs.SceneAssets;
using NGame.SceneAssets;
using NGame.SceneAssets.Implementations;

namespace NGame.Core.Tests.Ecs.SceneAssets;



public class SceneDeserializerOptionsFactoryTests
{
	[Component(Discriminator = "TestComponent")]
	public class TestComponent : EntityComponent
	{
		public int TestValue { get; init; }
	}



	private static SceneDeserializerOptionsFactory CreateOptionsFactory() =>
		new(
			new JsonConverter[]
			{
				new AssetIdConverter(),
				new SemVersionConverter()
			}
		);


	[Fact]
	public void CreateTypeInfoModifier_SimpleType_CreatesCorrectType()
	{
		// Arrange
		var json =
			"""
			{
			  "$type": "TestComponent",
			  "Id": "39E36192-5447-4C11-BF48-24319A0882D0",
			  "TestValue": 4
			}
			""";


		var optionsFactory = CreateOptionsFactory();
		var options = optionsFactory.Create(new[] { typeof(TestComponent) });


		// Act
		var component = JsonSerializer.Deserialize<EntityComponent>(json, options)!;


		// Assert
		component.Should().BeOfType<TestComponent>();

		var testComponent = (TestComponent)component;
		testComponent.TestValue.Should().Be(4);
	}


	[Fact]
	public void CreateTypeInfoModifier_NestedType_CreatesCorrectType()
	{
		// Arrange
		var json =
			"""
			{
			  "$type": "NGame.SceneAsset",
			  "Id": "0F85A235-5A85-4BFB-8BCB-2B7CAF7BF8CC",
			  "SerializerVersion": "0.0.1",
			  "Entities": [
			    {
			      "Id": "251D97B8-433B-4618-BD06-72898C91D86F",
			      "Name": "First Entity",
			      "Components": [
			        {
			          "$type": "TestComponent",
			          "Id": "39E36192-5447-4C11-BF48-24319A0882D0",
			          "TestValue": 4
			        }
			      ]
			    }
			  ]
			}
			""";


		var optionsFactory = CreateOptionsFactory();
		var options = optionsFactory.Create(new[] { typeof(TestComponent) });


		// Act
		var sceneAsset = JsonSerializer.Deserialize<SceneAsset>(json, options);


		// Assert
		sceneAsset.Should().NotBe(null);

		var entityEntry = sceneAsset!.Entities.Single(x => x.Id == Guid.Parse("251D97B8-433B-4618-BD06-72898C91D86F"));
		entityEntry.Should().NotBe(null);

		var firstComponent = entityEntry.Components.First();
		firstComponent.Should().BeOfType<TestComponent>();

		var testComponent = (TestComponent)firstComponent;
		testComponent.TestValue.Should().Be(4);
	}
}
