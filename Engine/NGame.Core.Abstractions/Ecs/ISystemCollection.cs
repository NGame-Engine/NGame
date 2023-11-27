namespace NGame.Ecs;



public interface ISystemCollection
{
	void AddEntity(Entity entity);
	void RemoveEntity(Entity entity);

	void AddComponent(EntityComponent sender);
	void RemoveComponent(EntityComponent sender);
}
