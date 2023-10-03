using System.Numerics;
using NGame.NGameSystem.Parallelism;
using NGame.Scenes;
using NGame.UpdateSchedulers;

namespace NGame.Components.Transforms;



internal class TransformProcessor : IDrawable
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


	public void Draw(GameTime gameTime)
	{
		var rootScene = _rootSceneAccessor.RootScene;
		UpdateSceneMatricesRecursive(rootScene, Matrix4x4.Identity);
	}


	private void UpdateSceneMatricesRecursive(Scene scene, Matrix4x4 parentMatrix)
	{
		scene.WorldMatrix = parentMatrix;
		scene.WorldMatrix.Translation += scene.Offset;

		_taskScheduler.Run(
			scene.RootEntities.Select(x => x.Transform),
			x => _matrixUpdater.UpdateWorldMatricesRecursive(x, scene.WorldMatrix)
		);

		foreach (var child in scene.Children)
		{
			UpdateSceneMatricesRecursive(child, scene.WorldMatrix);
		}
	}
}
