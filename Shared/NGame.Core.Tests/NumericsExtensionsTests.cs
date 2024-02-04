using System.Numerics;

namespace NGame.Core.Tests;



public class NumericsExtensionsTests
{
	[Fact]
	public void ToEulerAngles_XAxis_TransfersCorrectly()
	{
		// Arrange
		var eulerAngles = new Vector3(43, 0, 0);


		// Act
		var quaternion = eulerAngles.ToQuaternion();
		var result = quaternion.ToEulerAngles();


		// Assert
		Assert.Equal(eulerAngles.X, result.X, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Y, result.Y, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Z, result.Z, MathUtilities.FloatEpsilon);
	}


	[Fact]
	public void ToEulerAngles_YAxis_TransfersCorrectly()
	{
		// Arrange
		var eulerAngles = new Vector3(0, 74, 0);


		// Act
		var quaternion = eulerAngles.ToQuaternion();
		var result = quaternion.ToEulerAngles();


		// Assert
		Assert.Equal(eulerAngles.X, result.X, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Y, result.Y, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Z, result.Z, MathUtilities.FloatEpsilon);
	}


	[Fact]
	public void ToEulerAngles_ZAxis_TransfersCorrectly()
	{
		// Arrange
		var eulerAngles = new Vector3(0, 0, 47);


		// Act
		var quaternion = eulerAngles.ToQuaternion();
		var result = quaternion.ToEulerAngles();


		// Assert
		Assert.Equal(eulerAngles.X, result.X, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Y, result.Y, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Z, result.Z, MathUtilities.FloatEpsilon);
	}
}
