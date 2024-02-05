using NGame.Assets.Common.Ecs;
using Singulink.IO;

namespace NGameEditor.Backend.Scenes.SceneStates;



public class BackendScene(IAbsoluteFilePath filePath, SceneAsset sceneAsset)
{
	public IAbsoluteFilePath? FilePath { get; } = filePath;
	public SceneAsset SceneAsset { get; } = sceneAsset;
}
