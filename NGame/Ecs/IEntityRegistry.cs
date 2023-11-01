using NGame.Components;

namespace NGame.Ecs;



public interface IEntityRegistry
{
	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);
	void AddComponent(Entity entity, Component component);
	void RemoveComponent(Entity entity, Component component);
}
