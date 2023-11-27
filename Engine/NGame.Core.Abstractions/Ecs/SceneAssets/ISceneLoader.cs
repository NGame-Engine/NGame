using NGame.Assets;
using NGame.Parallelism;

namespace NGame.Ecs.SceneAssets;



public interface ISceneLoader
{
	IRunningOperation<float, Scene> Load(AssetId assetId);
	IRunningOperation<float, Scene> Unload(AssetId assetId);
}
