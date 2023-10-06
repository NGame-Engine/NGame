using NGame.Components;

namespace NGame.Services.Scripts;



public interface IComponentFactory
{
	T CreateComponent<T>(Entity entity) where T : Component;
}
