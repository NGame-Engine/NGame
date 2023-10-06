namespace NGame.Utilities;

public static class RandomExtensions
{
	public static float Between(this Random random, float minimum, float maximum) =>
		minimum + (random.NextSingle() * (maximum - minimum));



	public static byte NextByte(this Random random) =>
		(byte)random.Next(255);
}
