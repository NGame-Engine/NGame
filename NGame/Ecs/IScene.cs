using System.Numerics;
using NGame.Components;

namespace NGame.Ecs;



public interface IScene
{
	Guid Id { get; }

	IReadOnlyCollection<IScene> Children { get; }
	IReadOnlyCollection<Transform> Transforms { get; }
	IReadOnlyCollection<Transform> RootTransforms { get; }

	/// <summary>
	/// An offset applied to all entities of the scene relative to it's parent scene.
	/// </summary>
	Vector3 Offset { get; set; }

	/// <summary>
	/// The absolute transform applied to all entities of the scene.
	/// </summary>
	/// <remarks>This field is overwritten by the transform processor each frame.</remarks>
	Matrix4x4 WorldMatrix { get; set; }

	void SetParent(IScene scene);
	void AddChildScene(IScene scene);

	void AddTransform(Transform transform);
	void RemoveTransform(Transform transform);

	Entity CreateEntity();


	// TODO Remove blurb
	// The object obj is destroyed immediately after the current Update loop.
	// It destroys the GameObject, all its components and all transform children of the GameObject.
	// Actual object destruction is always delayed until after the current Update loop, but is always done before rendering.
	public void DestroyEntity(Entity entity);


	// TODO Remove blurb
	// The object obj is destroyed immediately after the current Update loop.
	// This method removes the component from the GameObject and destroys it.
	// Actual object destruction is always delayed until after the current Update loop, but is always done before rendering.
	// Note: calls OnDisable and OnDestroy before the script is removed.
	public void DestroyComponent(Component component);

	void NotifyComponentAdded(Entity entity, Component component);
	void NotifyComponentRemoved(Entity entity, Component component);
}



public static class SceneExtensions
{
	public static Entity CreateEntityAt(this IScene scene, Vector3 position)
	{
		var entity = scene.CreateEntity();
		entity.Transform.Position = position;
		return entity;
	}
}
