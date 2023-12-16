using System.Text.Json;

namespace NGame.SceneAssets;



public interface ISceneSerializerOptionsFactory
{
	JsonSerializerOptions Create(IEnumerable<Type> componentTypes);
}
