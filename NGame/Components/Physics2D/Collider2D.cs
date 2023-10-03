using System.Numerics;
using NGame.Ecs;

namespace NGame.Components.Physics2D;



public abstract class Collider2D : Component
{
	public Vector2 Offset { get; set; }
	public float Density { get; set; } = 1f;
	public float Bounce { get; set; } = .3f;
	public float Friction { get; set; } = .5f;
}
