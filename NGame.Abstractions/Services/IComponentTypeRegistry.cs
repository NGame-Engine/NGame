using NGame.Components;

namespace NGame.Services;



public interface IComponentTypeRegistry
{
	void Register(Type type);
	void Register<T>() where T : Component;
	ICollection<Type> GetComponentTypes();
}
