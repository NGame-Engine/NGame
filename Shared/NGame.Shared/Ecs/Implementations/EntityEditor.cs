namespace NGame.Ecs.Implementations;



public class EntityEditor : IEntityEditor
{
	public event Action<Entity>? EntityCreated;
	public event Action<Entity>? EntityRemoving;
	public event Action<EntityComponent>? ComponentAdded;
	public event Action<EntityComponent>? ComponentRemoving;


	public Entity CreateEntity(Scene scene)
	{
		var entity = new Entity(scene);
		scene.InternalTransforms.Add(entity);
		scene.InternalRootTransforms.Add(entity);
		EntityCreated?.Invoke(entity);
		return entity;
	}


	public Entity CreateChildEntity(Entity entity)
	{
		var scene = entity.Scene;
		var childEntity = new Entity(scene);
		scene.InternalTransforms.Add(entity);

		ChangeParent(childEntity, scene, entity);

		EntityCreated?.Invoke(entity);
		return childEntity;
	}


	public void SetParent(Entity child, Entity? newParent)
	{
		var newScene = newParent?.Scene ?? child.Scene;
		ChangeParent(child, newScene, newParent);
	}


	public void MoveToScene(Entity entity, Scene scene) =>
		ChangeParent(entity, scene, null);


	public void RemoveEntity(Entity entity)
	{
		EntityRemoving?.Invoke(entity);
		throw new NotImplementedException();
	}


	private void ChangeParent(Entity child, Scene newScene, Entity? newParent)
	{
		child.Parent?.InternalChildren.Remove(child);
		child.Parent = newParent;
		newParent?.InternalChildren.Add(child);


		child.Scene.InternalTransforms.Remove(child);
		child.Scene.InternalRootTransforms.Remove(child);

		child.Scene = newScene;
		newScene.InternalTransforms.Add(child);
		if (child.Parent == null) newScene.InternalRootTransforms.Add(child);
	}


	public void AddComponent(Entity entity, EntityComponent component)
	{
		entity.InternalComponents.Add(component);
		component.Entity = entity;
		ComponentAdded?.Invoke(component);
	}


	public void RemoveComponent(Entity entity, EntityComponent component)
	{
		ComponentRemoving?.Invoke(component);
		entity.InternalComponents.Remove(component);
		component.Entity = null;
	}
}
