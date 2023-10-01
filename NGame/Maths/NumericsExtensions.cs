using System.Numerics;

namespace NGame.Maths;



public static class NumericsExtensions
{
	public static Quaternion ToQuaternion(this Vector3 vector3)
	{

		float cy = (float)Math.Cos(vector3.Z * 0.5);
		float sy = (float)Math.Sin(vector3.Z * 0.5);
		float cp = (float)Math.Cos(vector3.Y * 0.5);
		float sp = (float)Math.Sin(vector3.Y * 0.5);
		float cr = (float)Math.Cos(vector3.X * 0.5);
		float sr = (float)Math.Sin(vector3.X * 0.5);

		return new Quaternion
		{
			W = (cr * cp * cy + sr * sp * sy),
			X = (sr * cp * cy - cr * sp * sy),
			Y = (cr * sp * cy + sr * cp * sy),
			Z = (cr * cp * sy - sr * sp * cy)
		};

	}

	public static Vector3 ToEulerAngles(this Quaternion quaternion)
	{
		Vector3 angles = new();

		// roll / x
		double sinr_cosp = 2 * (quaternion.W * quaternion.X + quaternion.Y * quaternion.Z);
		double cosr_cosp = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
		angles.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

		// pitch / y
		double sinp = 2 * (quaternion.W * quaternion.Y - quaternion.Z * quaternion.X);
		if (Math.Abs(sinp) >= 1)
		{
			angles.Y = (float)Math.CopySign(Math.PI / 2, sinp);
		}
		else
		{
			angles.Y = (float)Math.Asin(sinp);
		}

		// yaw / z
		double siny_cosp = 2 * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
		double cosy_cosp = 1 - 2 * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
		angles.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

		return angles;
	}
}
