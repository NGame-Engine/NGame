namespace NGame.NGameSystem;



public static class MathUtilities
{
	public const double FloatEpsilon = 2.38418579E-07f;

	public const double DegreesToRadiansFactor = Math.PI / 180;
	public const double RadiansToDegreesFactor = 180 / Math.PI;


	public static float DegreesToRadians(float degrees) =>
		(float)DegreesToRadiansFactor * degrees;


	public static float RadiansToDegrees(float radians) =>
		(float)RadiansToDegreesFactor * radians;
}
