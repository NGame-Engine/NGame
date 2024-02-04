using NGame.Ecs;
using NGame.Parallelism;

namespace NGame.Platform.Ecs.SceneAssets;



public class SceneLoader(
	ISceneLoadOperationExecutor sceneLoadOperationExecutor,
	ISceneUnloadOperationExecutor sceneUnloadOperationExecutor,
	IOperationRequestScheduler operationRequestScheduler
)
	: ISceneLoader
{
	public IRunningOperation<float, Scene> Load(Guid assetId)
	{
		var operation = new OperationRequest<float, Scene>(
			0f,
			x => sceneLoadOperationExecutor.Execute(x, assetId)
		);

		return operationRequestScheduler.Schedule(operation);
	}


	public IRunningOperation<float, Scene> Unload(Guid assetId)
	{
		var operation = new OperationRequest<float, Scene>(
			0f,
			x => sceneUnloadOperationExecutor.Execute(x, assetId)
		);

		return operationRequestScheduler.Schedule(operation);
	}
}
