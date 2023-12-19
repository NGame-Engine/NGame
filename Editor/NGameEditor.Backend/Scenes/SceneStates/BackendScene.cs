using NGame.SceneAssets;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Scenes.SceneStates;



public class BackendScene
{
	public BackendScene(AbsolutePath? filePath, SceneAsset sceneAsset)
	{
		FilePath = filePath;
		SceneAsset = sceneAsset;
	}


	public AbsolutePath? FilePath { get; }
	public SceneAsset SceneAsset { get; }
}
