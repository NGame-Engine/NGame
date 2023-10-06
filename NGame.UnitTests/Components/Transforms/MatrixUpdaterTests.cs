using System.Numerics;
using NGame.Assets;
using NGame.Ecs;
using NGame.Services.Transforms;
using NGame.Utilities;
using NSubstitute;

namespace NGame.UnitTests.Components.Transforms;



public class MatrixUpdaterTests
{
	private static IMatrixUpdater Create() => new MatrixUpdater();


	private static Scene CreateScene()
	{
		var entityRegistry = Substitute.For<IEntityRegistry>();


		return new Scene(entityRegistry);
	}


	[Fact]
	public void UpdateWorldMatricesRecursive_PositionChanges_AreCorrect()
	{
		// Arrange
		var matrixUpdater = Create();

		var scene = CreateScene();
		var rootEntity = scene.CreateEntity();

		var childEntity = rootEntity.CreateChildEntity();

		for (int i = 0; i < 1000; i++)
		{
			var nextChild = childEntity.CreateChildEntity();
			nextChild.Transform.Position = new Vector3(1, .5f, 0);

			childEntity = nextChild;
		}


		// Act
		matrixUpdater.UpdateWorldMatricesRecursive(rootEntity.Transform, Matrix4x4.Identity);


		// Assert
		var childPosition = childEntity.Transform.WorldMatrix.Translation;

		Assert.Equal(1000f, childPosition.X);
		Assert.Equal(500f, childPosition.Y);
	}


	[Fact]
	public void UpdateWorldMatricesRecursive_RotationChanges_AreCorrect()
	{
		// Arrange
		var matrixUpdater = Create();

		var scene = CreateScene();
		var rootEntity = scene.CreateEntity();
		rootEntity.Transform.EulerAngles = Vector3.UnitZ * -90;

		var childEntity = rootEntity.CreateChildEntity();
		childEntity.Transform.Position = new Vector3(4, 0, 0);


		// Act
		matrixUpdater.UpdateWorldMatricesRecursive(rootEntity.Transform, Matrix4x4.Identity);


		// Assert
		var childPosition = childEntity.Transform.WorldMatrix.Translation;

		Assert.Equal(0f, childPosition.X, 2.38418579E-07f);
		Assert.Equal(-4, childPosition.Y, 2.38418579E-07f);
		Assert.Equal(0, childPosition.Z, 2.38418579E-07f);
	}


	[Fact]
	public void UpdateWorldMatricesRecursive_ManyRotationChanges_AreCorrect()
	{
		// Arrange
		var matrixUpdater = Create();

		var scene = CreateScene();
		var rootEntity = scene.CreateEntity();

		var childEntity = rootEntity.CreateChildEntity();

		var vectorA = new Vector3(0, 0, 90);
		var vectorB = new Vector3(0, 0, -90);


		var alternate = true;
		for (int i = 0; i < 10; i++)
		{
			var nextChild = childEntity.CreateChildEntity();
			nextChild.Transform.Position = new Vector3(1, 0, 0);

			var rotation =
				alternate
					? vectorA
					: vectorB;

			nextChild.Transform.Rotation = rotation.ToQuaternion();

			childEntity = nextChild;
			alternate = !alternate;
		}


		// Act
		matrixUpdater.UpdateWorldMatricesRecursive(rootEntity.Transform, Matrix4x4.Identity);


		// Assert
		var childPosition = childEntity.Transform.WorldMatrix.Translation;

		Assert.Equal(5f, childPosition.X, 2.38418579E-04f);
		Assert.Equal(5f, childPosition.Y, 2.38418579E-04f);
		Assert.Equal(0, childPosition.Z, 2.38418579E-07f);
	}
}
