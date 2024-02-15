using NGame.Parallelism;

namespace NGame.Ecs;



public interface ISceneLoader
{
	Scene Load(Guid assetId);
	IRunningOperation<float, Scene> StartLoading(Guid assetId);
	IRunningOperation<float, Scene> StartUnloading(Guid assetId);
}
