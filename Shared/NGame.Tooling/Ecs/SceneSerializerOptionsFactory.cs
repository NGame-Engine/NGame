using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using NGame.Ecs;

namespace NGame.Tooling.Ecs;



public class SceneSerializerOptionsFactory(
	IEnumerable<JsonConverter> jsonConverters
) : ISceneSerializerOptionsFactory
{
	public JsonSerializerOptions Create(IEnumerable<Type> componentTypes)
	{
		var options = new JsonSerializerOptions
		{
			TypeInfoResolver = CreateTypeInfoResolver(componentTypes)
		};

		foreach (var jsonConverter in jsonConverters)
		{
			options.Converters.Add(jsonConverter);
		}

		return options;
	}


	private static DefaultJsonTypeInfoResolver CreateTypeInfoResolver(IEnumerable<Type> componentTypes)
	{
		var polymorphismOptions = CreatePolymorphismOptions(componentTypes);


		return new DefaultJsonTypeInfoResolver
		{
			Modifiers =
			{
				x =>
				{
					if (x.Type != typeof(EntityComponent)) return;
					x.PolymorphismOptions = polymorphismOptions;
				}
			}
		};
	}


	private static JsonPolymorphismOptions CreatePolymorphismOptions(IEnumerable<Type> componentTypes)
	{
		var jsonPolymorphismOptions = new JsonPolymorphismOptions
		{
			IgnoreUnrecognizedTypeDiscriminators = true,
			UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
		};

		foreach (var type in componentTypes)
		{
			if (type.IsAssignableTo(typeof(EntityComponent)) == false)
			{
				var message = $"Type {type} is not a subtype of {nameof(EntityComponent)}";
				throw new InvalidOperationException(message);
			}

			var typeDiscriminator = ComponentAttribute.GetDiscriminator(type);
			var jsonDerivedType = new JsonDerivedType(type, typeDiscriminator);
			jsonPolymorphismOptions.DerivedTypes.Add(jsonDerivedType);
		}

		return jsonPolymorphismOptions;
	}
}
