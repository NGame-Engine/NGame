using System.Numerics;
using SFML.System;

namespace NGamePlugin.Audio.Sfml;



public static class Extensions
{
	public static Vector3f ToSfmlVector3(this Vector3 vector3) =>
		new(vector3.X, vector3.Y, vector3.Z);
}
