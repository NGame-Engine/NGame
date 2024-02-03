using NGame.Tooling.Ecs;
using NGameEditor.Bridge.Shared;

namespace NGameEditor.Backend.Scenes.SceneStates;



public class BackendScene(AbsolutePath? filePath, SceneAsset sceneAsset)
{
	public AbsolutePath? FilePath { get; } = filePath;
	public SceneAsset SceneAsset { get; } = sceneAsset;
}
