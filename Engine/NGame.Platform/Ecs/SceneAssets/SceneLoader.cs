using NGame.Assets;
using NGame.Ecs;
using NGame.Parallelism;

namespace NGame.Implementations.Ecs.SceneAssets;



public class SceneLoader : ISceneLoader
{
	private readonly ISceneLoadOperationExecutor _sceneLoadOperationExecutor;
	private readonly ISceneUnloadOperationExecutor _sceneUnloadOperationExecutor;
	private readonly IOperationRequestScheduler _operationRequestScheduler;


	public SceneLoader(
		ISceneLoadOperationExecutor sceneLoadOperationExecutor,
		ISceneUnloadOperationExecutor sceneUnloadOperationExecutor,
		IOperationRequestScheduler operationRequestScheduler
	)
	{
		_sceneLoadOperationExecutor = sceneLoadOperationExecutor;
		_sceneUnloadOperationExecutor = sceneUnloadOperationExecutor;
		_operationRequestScheduler = operationRequestScheduler;
	}


	public IRunningOperation<float, Scene> Load(AssetId assetId)
	{
		var operation = new OperationRequest<float, Scene>(
			0f,
			x => _sceneLoadOperationExecutor.Execute(x, assetId)
		);

		return _operationRequestScheduler.Schedule(operation);
	}


	public IRunningOperation<float, Scene> Unload(AssetId assetId)
	{
		var operation = new OperationRequest<float, Scene>(
			0f,
			x => _sceneUnloadOperationExecutor.Execute(x, assetId)
		);

		return _operationRequestScheduler.Schedule(operation);
	}
}
