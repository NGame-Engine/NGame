using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Bridge.InterProcessCommunication;



public interface IFrontendApi
{
	void SetProjectInformation(ProjectInformation projectInformation);
	void UpdateFiles(DirectoryDescription rootDirectory);
	void UpdateLoadedScene(SceneDescription sceneDescription);
}
