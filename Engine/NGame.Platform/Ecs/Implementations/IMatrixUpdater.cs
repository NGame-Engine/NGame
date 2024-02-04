using System.Numerics;
using NGame.Ecs;

namespace NGame.Platform.Ecs.Implementations;



public interface IMatrixUpdater
{
	void UpdateWorldMatricesRecursive(Entity transform, Matrix4x4 parentWorldMatrix);
	void UpdateLocalFromWorld(Entity transform);
}
