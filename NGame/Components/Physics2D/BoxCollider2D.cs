using System.Numerics;

namespace NGame.Components.Physics2D;



public class BoxCollider2D : Collider2D
{
	public Vector2 Size { get; set; } = Vector2.One;
}
