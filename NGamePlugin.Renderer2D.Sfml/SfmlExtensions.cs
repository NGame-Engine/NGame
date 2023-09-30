using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace NGamePlugin.Renderer2D.Sfml;



public static class SfmlExtensions
{
	public static Vector2f ToSfmlVector2(this Vector2 vector2) =>
		new(vector2.X, vector2.Y);


	public static Vector2f ToSfmlVector2(this Vector3 vector3) =>
		new(vector3.X, vector3.Y);


	public static Color ToSfmlColor(this System.Drawing.Color color) =>
		new(color.R, color.G, color.B, color.A);
}
