using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using NGame.Assets;

namespace NGame.UnitTests.Assets;

public class PolymorphicTypeResolverTests
{
	public class BaseComponent
	{
	}



	public class DerivedComponent : BaseComponent
	{
	}



	[Fact]
	public void GetTypeInfo_TypeEntry_IsUsed()
	{
		// Arrange
		var discriminator = "my-discriminator";
		var input = $"{{\"$type\": \"{discriminator}\"}}";

		var polymorphisms = new Dictionary<Type, JsonPolymorphismOptions>
		{
			[typeof(BaseComponent)] = new()
			{
				IgnoreUnrecognizedTypeDiscriminators = true,
				UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
				DerivedTypes = { new JsonDerivedType(typeof(DerivedComponent), discriminator) }
			}
		};


		var resolver = new PolymorphicTypeResolver(polymorphisms);


		// Act
		var testComponent =
			JsonSerializer.Deserialize<BaseComponent>(
				input,
				new JsonSerializerOptions { TypeInfoResolver = resolver }
			)!;


		// Assert
		Assert.IsType<DerivedComponent>(testComponent);
	}
}
