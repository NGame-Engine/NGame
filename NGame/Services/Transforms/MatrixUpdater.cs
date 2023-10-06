using System.Numerics;
using NGame.Components;

namespace NGame.Services.Transforms;



internal interface IMatrixUpdater
{
	void UpdateWorldMatricesRecursive(Transform transform, Matrix4x4 parentWorldMatrix);
	void UpdateLocalFromWorld(Transform transform);
}



internal class MatrixUpdater : IMatrixUpdater
{
	public void UpdateWorldMatricesRecursive(Transform transform, Matrix4x4 parentWorldMatrix)
	{
		transform.WorldMatrix = Matrix4x4.Multiply(transform.LocalMatrix, parentWorldMatrix);

		foreach (var child in transform.Children)
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
	public void UpdateLocalFromWorld(Transform transform)
	{
		var worldMatrix =
			transform.Parent?.WorldMatrix ??
			transform.Entity.Scene.WorldMatrix;

		Matrix4x4.Invert(worldMatrix, out var inverseTransform);
		transform.LocalMatrix = transform.WorldMatrix * inverseTransform;
	}
}
