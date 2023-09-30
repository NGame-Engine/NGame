using System.Numerics;
using NGame.Assets;
using NGame.Ecs;

namespace NGame.Transforms;

[Component(StableDiscriminator = "Transform")]
public class Transform : IComponent
{
	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Scale;
}
