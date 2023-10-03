using System.Numerics;
using NGame.Assets;
using NGame.Ecs;
using NGame.NGameSystem;

namespace NGame.Components.Transforms;



[Component(StableDiscriminator = "Transform")]
public sealed class Transform
{
	private readonly List<Transform> _children = new();


	public Transform(Entity entity)
	{
		Entity = entity;
	}


	public readonly Entity Entity;

	public Matrix4x4 WorldMatrix = Matrix4x4.Identity;
	public Matrix4x4 LocalMatrix = Matrix4x4.Identity;


	/// <summary>
	/// The translation relative to the parent transformation.
	/// </summary>
	public Vector3 Position
	{
		get => LocalMatrix.Translation;
		set => LocalMatrix.Translation = value;
	}


	/// <summary>
	/// The rotation relative to the parent transformation.
	/// </summary>
	public Quaternion Rotation
	{
		get => Quaternion.CreateFromRotationMatrix(LocalMatrix);
		set => LocalMatrix = GenerateMatrix(Position, value, Scale);
	}


	/// <summary>
	/// The rotation relative to the parent transformation in Euler angles.
	/// </summary>
	public Vector3 EulerAngles
	{
		get => Rotation.ToEulerAngles();
		set => Rotation = value.ToQuaternion();
	}


	/// <summary>
	/// The scaling relative to the parent transformation.
	/// </summary>
	public Vector3 Scale
	{
		get
		{
			Matrix4x4.Decompose(LocalMatrix, out var scale, out _, out _);
			return scale;
		}
		set => LocalMatrix = GenerateMatrix(Position, Rotation, value);
	}


	public Transform? Parent { get; private set; }
	public IReadOnlyCollection<Transform> Children => _children;


	public void SetParent(Transform? newParent)
	{
		if (newParent == Parent) return;

		if (newParent != null)
		{
			var parentEntity = newParent.Entity;
			var otherScene = parentEntity.Scene;
			otherScene.AddEntity(Entity);
		}

		Parent?._children.Remove(this);
		newParent?._children.Add(this);

		Parent = newParent;
	}


	private Matrix4x4 GenerateMatrix(Vector3 position, Quaternion rotation, Vector3 scale)
	{
		var scaled = Matrix4x4.CreateScale(scale);
		var rotated = Matrix4x4.Transform(scaled, rotation);
		var translation = Matrix4x4.CreateTranslation(position);
		return rotated * translation;
	}
}
