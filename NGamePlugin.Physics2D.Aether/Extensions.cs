using NGame.Components.Physics2D;
using nkast.Aether.Physics2D.Common;
using nkast.Aether.Physics2D.Dynamics;
using Vector3 = System.Numerics.Vector3;

namespace NGamePlugin.Physics2D.Aether;



public static class Extensions
{
	public static Vector2 ToAetherVector2(this System.Numerics.Vector2 vector2) =>
		new(vector2.X, vector2.Y);


	public static System.Numerics.Vector2 ToNgVector2(this Vector2 vector2) =>
		new(vector2.X, vector2.Y);


	public static Vector2 ToAetherVector2(this Vector3 vector3) =>
		new(vector3.X, vector3.Y);


	public static BodyType ToAetherBodyType(this BodyType2D bodyType2D) =>
		bodyType2D switch
		{
			BodyType2D.Static => BodyType.Static,
			BodyType2D.Kinematic => BodyType.Kinematic,
			BodyType2D.Dynamic => BodyType.Dynamic,
			_ => throw new NotSupportedException($"BodyType '{bodyType2D}'")
		};


	public static Category ToAetherCategory(this Layer2D layer2D) =>
		(Category)(int)layer2D;
}
