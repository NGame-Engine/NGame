namespace NGame.Ecs;



public interface IEntityEditor
{
	event Action<Entity> EntityCreated;
	event Action<Entity> EntityRemoving;
	event Action<EntityComponent> ComponentAdded;
	event Action<EntityComponent> ComponentRemoving;


	Entity CreateEntity(Scene scene);
	Entity CreateChildEntity(Entity entity);
	void SetParent(Entity child, Entity? newParent);
	void MoveToScene(Entity entity, Scene scene);
	void RemoveEntity(Entity entity);
	void AddComponent(Entity entity, EntityComponent component);
	void RemoveComponent(Entity entity, EntityComponent component);
}
