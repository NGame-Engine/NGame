using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace NGame.Assets;

public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
	private readonly Dictionary<Type, JsonPolymorphismOptions> _polymorphisms;


	public PolymorphicTypeResolver(Dictionary<Type, JsonPolymorphismOptions> polymorphisms)
	{
		_polymorphisms = polymorphisms;
	}


	public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
	{
		JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);


		if (_polymorphisms.TryGetValue(jsonTypeInfo.Type, out var polymorphism))
		{
			jsonTypeInfo.PolymorphismOptions = polymorphism;
		}

		return jsonTypeInfo;
	}
}
