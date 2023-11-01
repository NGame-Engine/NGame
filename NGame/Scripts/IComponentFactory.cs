using NGame.Components;
using NGame.Ecs;

namespace NGame.Scripts;



public interface IComponentFactory
{
	T CreateComponent<T>(Entity entity) where T : Component;
}
