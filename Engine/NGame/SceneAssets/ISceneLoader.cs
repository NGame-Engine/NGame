using NGame.Assets;
using NGame.Ecs;
using NGame.Parallelism;

namespace NGame.SceneAssets;



public interface ISceneLoader
{
	IRunningOperation<float, Scene> Load(AssetId assetId);
	IRunningOperation<float, Scene> Unload(AssetId assetId);
}
