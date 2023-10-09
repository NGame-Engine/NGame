using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace NGame.Assets;

public static class PolymorphismOptions
{
	public static JsonPolymorphismOptions ForComponents(IEnumerable<Type> componentTypes)
	{
		var jsonPolymorphismOptions = new JsonPolymorphismOptions
		{
			IgnoreUnrecognizedTypeDiscriminators = true,
			UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
		};

		var derivedTypes =
			componentTypes
				.Select(x => new JsonDerivedType(x, GetTypeDiscriminator(x)));

		foreach (var derivedType in derivedTypes)
		{
			jsonPolymorphismOptions.DerivedTypes.Add(derivedType);
		}


		return jsonPolymorphismOptions;
	}


	private static string GetTypeDiscriminator(Type x) =>
		GetComponentAttribute(x)?.StableDiscriminator ?? x.FullName!;


	private static ComponentAttribute? GetComponentAttribute(ICustomAttributeProvider type) =>
		type
			.GetCustomAttributes(false)
			.Where(x => x.GetType() == typeof(ComponentAttribute))
			.Cast<ComponentAttribute>()
			.FirstOrDefault();
}
