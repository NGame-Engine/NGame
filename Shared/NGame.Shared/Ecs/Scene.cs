using System.Numerics;

namespace NGame.Ecs;



public class Scene
{
	internal readonly HashSet<Scene> InternalChildren = new();
	internal readonly HashSet<Entity> InternalTransforms = new();
	internal readonly HashSet<Entity> InternalRootTransforms = new();


	public Guid Id { get; init; } = Guid.NewGuid();


	public Scene? Parent { get; internal set; }


	public IReadOnlyCollection<Scene> Children => InternalChildren;
	public IReadOnlyCollection<Entity> Transforms => InternalTransforms;
	public IReadOnlyCollection<Entity> RootTransforms => InternalRootTransforms;

	/// <summary>
	/// An offset applied to all entities of the scene relative to it's parent scene.
	/// </summary>
	public Vector3 Offset { get; set; }

	/// <summary>
	/// The absolute transform applied to all entities of the scene.
	/// </summary>
	/// <remarks>This field is overwritten by the transform processor each frame.</remarks>
	public Matrix4x4 WorldMatrix { get; set; }
}
