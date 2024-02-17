using System.Numerics;

namespace NGame.Core.Tests;



public class NumericsExtensionsTests
{
	[Fact]
	public void GetYaw_SomeValue_IsCorrect()
	{
		// Arrange
		var yaw = 15f;
		var yawInRadians = MathUtilities.DegreesToRadians(yaw);

		var quaternion = Quaternion.CreateFromYawPitchRoll(
			yawInRadians,
			0,
			0
		);


		// Act
		var result = quaternion.GetYaw();


		// Assert
		Assert.Equal(yawInRadians, result, MathUtilities.FloatEpsilon);
	}


	[Fact]
	public void GetPitch_SomeValue_IsCorrect()
	{
		// Arrange
		var pitch = 15f;
		var pitchInRadians = MathUtilities.DegreesToRadians(pitch);

		var quaternion = Quaternion.CreateFromYawPitchRoll(
			0,
			pitchInRadians,
			0
		);


		// Act
		var result = quaternion.GetPitch();


		// Assert
		Assert.Equal(pitchInRadians, result, MathUtilities.FloatEpsilon);
	}


	[Fact]
	public void GetRoll_SomeValue_IsCorrect()
	{
		// Arrange
		var roll = 15f;
		var rollInRadians = MathUtilities.DegreesToRadians(roll);

		var quaternion = Quaternion.CreateFromYawPitchRoll(
			0,
			0,
			rollInRadians
		);


		// Act
		var result = quaternion.GetRoll();


		// Assert
		Assert.Equal(rollInRadians, result, MathUtilities.FloatEpsilon);
	}


	[Fact]
	public void ToEulerAngles_XAxis_TransfersCorrectly()
	{
		// Arrange
		var eulerAngles = new Vector3(43, 0, 0);
		var quaternion = eulerAngles.ToQuaternion();


		// Act
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
		var quaternion = eulerAngles.ToQuaternion();


		// Act
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
		var quaternion = eulerAngles.ToQuaternion();


		// Act
		var result = quaternion.ToEulerAngles();


		// Assert

		Assert.Equal(eulerAngles.X, result.X, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Y, result.Y, MathUtilities.FloatEpsilon);
		Assert.Equal(eulerAngles.Z, result.Z, MathUtilities.FloatEpsilon);
	}
}
