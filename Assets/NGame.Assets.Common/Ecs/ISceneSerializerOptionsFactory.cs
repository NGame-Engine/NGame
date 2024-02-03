using System.Text.Json;

namespace NGame.Tooling.Ecs;



public interface ISceneSerializerOptionsFactory
{
	JsonSerializerOptions Create(IEnumerable<Type> componentTypes);
}
