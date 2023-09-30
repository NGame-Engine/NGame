using System.Numerics;
using NGame.Assets;
using NGame.Ecs;

namespace NGame.Components.Transforms;

[Component(StableDiscriminator = "Transform")]
public sealed class Transform : IComponent
{
	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Scale;
}
