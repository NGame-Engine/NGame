namespace NGame.Components.Physics2D;



/// <summary>The body type.</summary>
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
	Dynamic,
}
