using System.Numerics;

namespace NGame.Components.Physics2D;



public struct PhysicsBody2DTransformResult
{
	public readonly Vector2 Position;
	public readonly float Rotation;


	public PhysicsBody2DTransformResult(Vector2 position, float rotation)
	{
		Position = position;
		Rotation = rotation;
	}
}
