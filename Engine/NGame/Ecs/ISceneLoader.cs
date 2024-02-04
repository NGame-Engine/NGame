using NGame.Parallelism;

namespace NGame.Ecs;



public interface ISceneLoader
{
	IRunningOperation<float, Scene> Load(Guid assetId);
	IRunningOperation<float, Scene> Unload(Guid assetId);
}
