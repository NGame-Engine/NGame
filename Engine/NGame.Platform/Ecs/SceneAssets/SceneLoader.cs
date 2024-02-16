using NGame.Ecs;
using NGame.Parallelism;

namespace NGame.Platform.Ecs.SceneAssets;



internal class SceneLoader(
	ISceneLoadOperationExecutor sceneLoadOperationExecutor,
	ISceneUnloadOperationExecutor sceneUnloadOperationExecutor,
	IOperationRequestScheduler operationRequestScheduler
)
	: ISceneLoader
{
	public Scene Load(Guid assetId) =>
		sceneLoadOperationExecutor.Execute(_ => { }, assetId);


	public IRunningOperation<float, Scene> StartLoading(Guid assetId)
	{
		var operation = new OperationRequest<float, Scene>(
			0f,
			x => sceneLoadOperationExecutor.Execute(x, assetId)
		);

		return operationRequestScheduler.Schedule(operation);
	}


	public IRunningOperation<float, Scene> StartUnloading(Guid assetId)
	{
		var operation = new OperationRequest<float, Scene>(
			0f,
			x => sceneUnloadOperationExecutor.Execute(x, assetId)
		);

		return operationRequestScheduler.Schedule(operation);
	}
}
