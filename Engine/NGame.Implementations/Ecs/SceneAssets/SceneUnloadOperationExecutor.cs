using NGame.Assets;
using NGame.Ecs;

namespace NGame.Core.Ecs.SceneAssets;



public interface ISceneUnloadOperationExecutor
{
	Scene Execute(Action<float> updateProgress, AssetId assetId);
}



public class SceneUnloadOperationExecutor : ISceneUnloadOperationExecutor
{
	public Scene Execute(Action<float> updateProgress, AssetId assetId)
	{
		throw new NotImplementedException();
	}
}
