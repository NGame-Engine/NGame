using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using NGame.Assets;
using NGame.Ecs;

namespace NGame.UnitTests.Assets;

public class PolymorphismOptionsTests
{
	public class TestComponent : IComponent
	{
	}



	[Fact]
	public void ForComponents_DerivedType_ResolvesCorrectType()
	{
		// Arrange
		var input = """
		            {
		              "$type": "NGame.UnitTests.Assets.PolymorphismOptionsTests+TestComponent"
		            }
		            """;
		var types = new[]
		{
			typeof(TestComponent)
		};


		// Act
		var polymorphismOptions = PolymorphismOptions.ForComponents(types);

		var resolver =
			new PolymorphicTypeResolver(
				new Dictionary<Type, JsonPolymorphismOptions>
				{
					[typeof(IComponent)] = polymorphismOptions
				}
			);


		var testComponent =
			JsonSerializer.Deserialize<IComponent>(
				input,
				new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase, TypeInfoResolver = resolver
				}
			)!;


		// Assert
		Assert.IsType<TestComponent>(testComponent);
	}



	[Component(StableDiscriminator = "jojojo")]
	public class TestComponentWithStableDiscriminator : IComponent
	{
	}



	[Fact]
	public void ForComponents_WithStableDiscriminator_ResolvesCorrectType()
	{
		// Arrange
		var input = """
		            {
		              "$type": "jojojo"
		            }
		            """;
		var types = new[]
		{
			typeof(TestComponentWithStableDiscriminator)
		};


		// Act
		var polymorphismOptions = PolymorphismOptions.ForComponents(types);

		var resolver =
			new PolymorphicTypeResolver(
				new Dictionary<Type, JsonPolymorphismOptions>
				{
					[typeof(IComponent)] = polymorphismOptions
				}
			);


		var testComponent =
			JsonSerializer.Deserialize<IComponent>(
				input,
				new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase, TypeInfoResolver = resolver
				}
			)!;


		// Assert
		Assert.IsType<TestComponentWithStableDiscriminator>(testComponent);
	}



	public class ContainingObject
	{
		public TestComponentWithStableDiscriminator InnerObject { get; set; }
	}



	[Fact]
	public void ForComponents_ContainedInOther_ResolvesCorrectType()
	{
		// Arrange
		var input = """
		            {
		              "innerObject": {
		                "$type": "jojojo"
		              }
		            }
		            """;


		var types = new[]
		{
			typeof(TestComponentWithStableDiscriminator)
		};


		// Act
		var polymorphismOptions = PolymorphismOptions.ForComponents(types);

		var testComponent =
			JsonSerializer.Deserialize<ContainingObject>(
				input,
				new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					TypeInfoResolver = new PolymorphicTypeResolver(
						new Dictionary<Type, JsonPolymorphismOptions>
						{
							[typeof(IComponent)] = polymorphismOptions
						}
					)
				}
			)!;


		// Assert
		Assert.IsType<TestComponentWithStableDiscriminator>(testComponent.InnerObject);
	}
}
