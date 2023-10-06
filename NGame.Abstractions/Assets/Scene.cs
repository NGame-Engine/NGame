using System.Numerics;
using NGame.Components;

namespace NGame.Assets;



public sealed class Scene
{
	private readonly List<Scene> _children = new();
	private readonly List<Entity> _entities = new();

	// TODO keep updated if parent added to entity
	// TODO keep updated if parent removed from entity
	// Or just straight up remove and always filter?
	private readonly List<Entity> _rootEntities = new();


	/// <summary>
	/// Initializes a new instance of the <see cref="Scene"/> class.
	/// </summary>
	public Guid Id = Guid.NewGuid();


	public Scene(IEntityRegistry entityRegistry)
	{
		EntityRegistry = entityRegistry;
	}


	public IEntityRegistry EntityRegistry { get; }

	public Scene? Parent { get; private set; }


	public IReadOnlyCollection<Scene> Children => _children;
	public IReadOnlyCollection<Entity> Entities => _entities;
	public IReadOnlyCollection<Entity> RootEntities => _rootEntities;


	/// <summary>
	/// An offset applied to all entities of the scene relative to it's parent scene.
	/// </summary>
	public Vector3 Offset;

	/// <summary>
	/// The absolute transform applied to all entities of the scene.
	/// </summary>
	/// <remarks>This field is overwritten by the transform processor each frame.</remarks>
	public Matrix4x4 WorldMatrix;


	public Entity CreateEntity()
	{
		var entity = new Entity(this);
		_entities.Add(entity);
		_rootEntities.Add(entity);
		EntityRegistry.AddEntity(entity);
		return entity;
	}


	// TODO add signal
	public Entity CreateChildEntity(Entity parent)
	{
		var child = new Entity(parent.Scene);
		child.Transform.SetParent(parent.Transform);
		return child;
	}


	public Entity Instantiate(Entity entity)
	{
		// TODO Create copy of entity
		throw new NotImplementedException();
	}


	// TODO extract out of scene, probably
	/// <summary>
	/// Adding Entity to a Scene will always make it a root Entity.
	/// </summary>
	public void AddEntity(Entity entity)
	{
		if (entity.Scene == this) return;

		entity.Scene._entities.Remove(entity);
		entity.Scene = this;
		_entities.Add(entity);

		entity.Transform.SetParent(null);
		_rootEntities.Add(entity);

		EntityRegistry.AddEntity(entity);
	}


	// TODO Remove blurb
	// The object obj is destroyed immediately after the current Update loop.
	// It destroys the GameObject, all its components and all transform children of the GameObject.
	// Actual object destruction is always delayed until after the current Update loop, but is always done before rendering.
	public void Destroy(Entity entity)
	{
		// TODO Destroy Entity
	}


	// TODO Remove blurb
	// The object obj is destroyed immediately after the current Update loop.
	// This method removes the component from the GameObject and destroys it.
	// Actual object destruction is always delayed until after the current Update loop, but is always done before rendering.
	// Note: calls OnDisable and OnDestroy before the script is removed.
	public void Destroy(Component component)
	{
		// TODO Destroy Component
	}
}
public static class SceneExtensions
{
	public static Entity CreateEntityAt(this Scene scene, Vector3 position)
	{
		var entity = scene.CreateEntity();
		entity.Transform.Position = position;
		return entity;
	}
}

