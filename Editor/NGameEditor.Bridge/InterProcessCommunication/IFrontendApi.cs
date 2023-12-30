using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IFrontendApi
{
	void UpdateFiles(DirectoryDescription rootDirectory);
	void UpdateLoadedScene(SceneDescription sceneDescription);
}
