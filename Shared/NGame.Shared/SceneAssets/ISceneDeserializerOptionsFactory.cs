using System.Text.Json;

namespace NGame.SceneAssets;



public interface ISceneDeserializerOptionsFactory
{
	JsonSerializerOptions Create(IEnumerable<Type> componentTypes);
}
