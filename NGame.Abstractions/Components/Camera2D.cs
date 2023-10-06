using System.Numerics;

namespace NGame.Components;



public class Camera2D : Component
{
	/// <summary>
	/// How much the camera sees, in meters.
	/// </summary>
	public Vector2 Size { get; set; } = new(800, 600);
}
