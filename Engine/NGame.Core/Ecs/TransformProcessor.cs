using System.Numerics;
using NGame.Ecs;
using NGame.Parallelism;
using NGame.UpdateLoop;

namespace NGame.Core.Ecs;



public class TransformProcessor : IDrawable
{
	private readonly ITaskScheduler _taskScheduler;
	private readonly IMatrixUpdater _matrixUpdater;
	private readonly IRootSceneAccessor _rootSceneAccessor;


	public TransformProcessor(
		ITaskScheduler taskScheduler,
		IMatrixUpdater matrixUpdater,
		IRootSceneAccessor rootSceneAccessor
	)
	{
		_taskScheduler = taskScheduler;
		_matrixUpdater = matrixUpdater;
		_rootSceneAccessor = rootSceneAccessor;
	}


	public int Order { get; set; } = 10000;


	public void Draw(GameTime gameTime)
	{
		var rootScene = _rootSceneAccessor.RootScene;
		UpdateSceneMatricesRecursive(rootScene, Matrix4x4.Identity);
	}


	private void UpdateSceneMatricesRecursive(Scene scene, Matrix4x4 parentMatrix)
	{
		var worldMatrix = parentMatrix;
		worldMatrix.Translation += scene.Offset;
		scene.WorldMatrix = worldMatrix;


		_taskScheduler.Run(
			scene.RootTransforms,
			x => _matrixUpdater.UpdateWorldMatricesRecursive(x, scene.WorldMatrix)
		);

		foreach (var child in scene.Children)
		{
			UpdateSceneMatricesRecursive(child, scene.WorldMatrix);
		}
	}
}
