using NGame.Components;

namespace NGame.Scripts;



public interface IComponentFactory
{
	T CreateComponent<T>(Entity entity) where T : Component;
}
