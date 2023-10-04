using System.Numerics;
using NGame.Ecs;

namespace NGame.Components.Cameras;



public class Camera2D : Component
{
	public Vector2 Size { get; set; } = new(800, 600);
}
