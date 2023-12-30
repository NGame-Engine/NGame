using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Bridge.Projects;


/// <summary>
/// Summary of all project information that is know immediately after starting the backend.
/// </summary>
public class ProjectInformation(
	List<ComponentTypeDefinition> componentTypeDefinitions
)
{
	public List<ComponentTypeDefinition> ComponentTypeDefinitions { get; } = componentTypeDefinitions;
}
