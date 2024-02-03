using NGame.Assets;
using NGame.Ecs;

namespace NGame.Implementations.SceneAssets;



public interface ISceneUnloadOperationExecutor
{
	// ReSharper disable UnusedParameter.Global
	Scene Execute(Action<float> updateProgress, AssetId assetId);
	// ReSharper restore UnusedParameter.Global
}



public class SceneUnloadOperationExecutor : ISceneUnloadOperationExecutor
{
	public Scene Execute(Action<float> updateProgress, AssetId assetId)
	{
		throw new NotImplementedException();
	}
}
