using NGame.Systems;

namespace NGame.Services;



public interface ISystemCollection
{
	void AddSystem(ISystem system);

	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);

	void AddComponent(Entity sender);
	void RemoveComponent(Entity sender);
}
