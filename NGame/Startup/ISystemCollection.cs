using NGame.Ecs;

namespace NGame.Startup;



public interface ISystemCollection
{
	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);

	void AddComponent(Entity sender);
	void RemoveComponent(Entity sender);
}
