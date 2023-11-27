using System.Text.Json;

namespace NGame.Assets;



public interface IAssetDeserializerOptionsFactory
{
	JsonSerializerOptions Create(IEnumerable<Type> assetTypes);
}
