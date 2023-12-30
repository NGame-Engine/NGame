using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IFrontendApi
{
	void UpdateLoadedScene(SceneDescription sceneDescription);
}
