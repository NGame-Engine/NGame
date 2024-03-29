using System.Numerics;

namespace NGame;



public static class NumericsExtensions
{
	public static Quaternion ToQuaternion(this Vector3 vector3) =>
		Quaternion.CreateFromYawPitchRoll(
			MathUtilities.DegreesToRadians(vector3.Y),
			MathUtilities.DegreesToRadians(vector3.X),
			MathUtilities.DegreesToRadians(vector3.Z)
		);


	public static Vector3 ToEulerAngles(this Quaternion quaternion) =>
		quaternion.ToEulerAnglesInRadians() * (float)MathUtilities.RadiansToDegreesFactor;


	public static Vector3 ToEulerAnglesInRadians(this Quaternion quaternion) =>
		new(
			GetPitch(quaternion),
			GetYaw(quaternion),
			GetRoll(quaternion)
		);


	/// <summary>
	/// Get the rotation around the Z axis in radians
	/// </summary>
	public static float GetYaw(this Quaternion quaternion)
	{
		var sinP = MathF.Sqrt(1 + 2 * (quaternion.W * quaternion.Y - quaternion.X * quaternion.Z));
		var cosP = MathF.Sqrt(1 - 2 * (quaternion.W * quaternion.Y - quaternion.X * quaternion.Z));
		return 2 * MathF.Atan2(sinP, cosP) - MathF.PI / 2;
	}


	/// <summary>
	/// Get the rotation around the Y axis in radians
	/// </summary>
	public static float GetPitch(this Quaternion quaternion)
	{
		var sinRCosP = 2 * (quaternion.W * quaternion.X + quaternion.Y * quaternion.Z);
		var cosRCosP = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
		return MathF.Atan2(sinRCosP, cosRCosP);
	}


	/// <summary>
	/// Get the rotation around the X axis in radians
	/// </summary>
	public static float GetRoll(this Quaternion quaternion)
	{
		var sinYCosP = 2 * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
		var cosYCosP = 1 - 2 * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
		return MathF.Atan2(sinYCosP, cosYCosP);
	}


	public static string ToLogString(this Matrix4x4 matrix4X4)
	{
		string Pad(float f) => f.ToString("F12").PadRight(15);
		return
			$"{Pad(matrix4X4.M11)} {Pad(matrix4X4.M12)} {Pad(matrix4X4.M13)} {Pad(matrix4X4.M14)}\n"
			+ $"{Pad(matrix4X4.M21)} {Pad(matrix4X4.M22)} {Pad(matrix4X4.M23)} {Pad(matrix4X4.M24)}\n"
			+ $"{Pad(matrix4X4.M31)} {Pad(matrix4X4.M32)} {Pad(matrix4X4.M33)} {Pad(matrix4X4.M34)}\n"
			+ $"{Pad(matrix4X4.M41)} {Pad(matrix4X4.M42)} {Pad(matrix4X4.M43)} {Pad(matrix4X4.M44)}";
	}
}
