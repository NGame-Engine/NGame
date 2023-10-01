using NGame.Components.Physics2D;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using BodyType = nkast.Aether.Physics2D.Dynamics.BodyType;

namespace NGamePlugin.Physics2D.Aether;



public static class Extensions
{
	public static Vector2 ToAetherVector2(this System.Numerics.Vector2 vector2) =>
		new(vector2.X, vector2.Y);


	public static System.Numerics.Vector2 ToNgVector2(this Vector2 vector2) =>
		new(vector2.X, vector2.Y);


	public static Vector2 ToAetherVector2(this System.Numerics.Vector3 vector3) =>
		new(vector3.X, vector3.Y);


	public static BodyType ToAetherBodyType(this NGame.Components.Physics2D.BodyType2D bodyType2D) =>
		bodyType2D switch
		{
			NGame.Components.Physics2D.BodyType2D.Static => BodyType.Static,
			NGame.Components.Physics2D.BodyType2D.Kinematic => BodyType.Kinematic,
			NGame.Components.Physics2D.BodyType2D.Dynamic => BodyType.Dynamic,
			_ => throw new NotSupportedException($"BodyType '{bodyType2D}'")
		};


	public static Category ToAetherCategory(this Layer2D layer2D) =>
		(Category)(int)layer2D;


	public static PhysicsBody2DTransformResult ToPhysicsBodyTransformResult(
		this Transform transform
	) =>
		new(
			transform.p.ToNgVector2(),
			transform.q.Phase
		);
}
