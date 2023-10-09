namespace NGame.Components;



public enum BodyType2D
{
	Unknown,

	/// <summary>
	/// Zero velocity, may be manually moved.
	/// </summary>
	Static,

	/// <summary>
	/// Zero mass, non-zero velocity set by user, moved by solver
	/// </summary>
	Kinematic,

	/// <summary>
	/// Positive mass, non-zero velocity determined by forces.
	/// </summary>
	Dynamic
}



public class PhysicsBody2D : Component
{
	public BodyType2D BodyType2D { get; set; } = BodyType2D.Dynamic;
}
