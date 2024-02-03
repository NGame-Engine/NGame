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
			GetRoll(quaternion),
			GetPitch(quaternion),
			GetYaw(quaternion)
		);


	/// <summary>
	/// Get the rotation around the X axis in radians
	/// </summary>
	private static float GetRoll(Quaternion quaternion)
	{
		double sinrCosp = 2 * (quaternion.W * quaternion.X + quaternion.Y * quaternion.Z);
		double cosrCosp = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
		return (float)Math.Atan2(sinrCosp, cosrCosp);
	}


	/// <summary>
	/// Get the rotation around the Y axis in radians
	/// </summary>
	private static float GetPitch(Quaternion quaternion)
	{
		double sinp = 2 * (quaternion.W * quaternion.Y - quaternion.Z * quaternion.X);
		if (Math.Abs(sinp) >= 1)
		{
			return (float)Math.CopySign(Math.PI / 2, sinp);
		}

		return (float)Math.Asin(sinp);
	}


	/// <summary>
	/// Get the rotation around the Z axis in radians
	/// </summary>
	private static float GetYaw(this Quaternion quaternion)
	{
		double sinyCosp = 2 * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
		double cosyCosp = 1 - 2 * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
		return (float)Math.Atan2(sinyCosp, cosyCosp);
	}
}
