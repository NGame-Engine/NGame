using System.Numerics;
using NGame.Ecs;

namespace NGame.Platform.Ecs.Implementations;



public class MatrixUpdater : IMatrixUpdater
{
	public void UpdateWorldMatricesRecursive(Entity transform, Matrix4x4 parentWorldMatrix)
	{
		transform.WorldMatrix = Matrix4x4.Multiply(transform.LocalMatrix, parentWorldMatrix);

		foreach (var child in transform.InternalChildren)
		{
			UpdateWorldMatricesRecursive(child, transform.WorldMatrix);
		}
	}


	// TODO needed for physics
	/// <summary>
	/// Updates the local matrix based on the world matrix and the parent entity's
	/// or containing scene's world matrix.
	/// (This is needed to keep the local matrix in sync after directly
	/// updating the world matrix).
	/// </summary>
	public void UpdateLocalFromWorld(Entity transform)
	{
		var worldMatrix =
			transform.Parent?.WorldMatrix ??
			transform.Scene.WorldMatrix;

		Matrix4x4.Invert(worldMatrix, out var inverseTransform);
		transform.LocalMatrix = transform.WorldMatrix * inverseTransform;
	}
}
