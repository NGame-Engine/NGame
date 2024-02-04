using System.Text.Json;

namespace NGame.Assets.Common.Ecs;



public interface ISceneSerializerOptionsFactory
{
	JsonSerializerOptions Create(IEnumerable<Type> componentTypes);
}
