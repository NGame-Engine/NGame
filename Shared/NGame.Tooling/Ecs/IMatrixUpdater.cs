using System.Numerics;

namespace NGame.Ecs;



public interface IMatrixUpdater
{
	void UpdateWorldMatricesRecursive(Entity transform, Matrix4x4 parentWorldMatrix);
	void UpdateLocalFromWorld(Entity transform);
}
