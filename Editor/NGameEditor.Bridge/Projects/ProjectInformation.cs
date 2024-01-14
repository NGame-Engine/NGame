using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Bridge.Projects;



/// <summary>
/// Summary of all project information that is know immediately after starting the backend.
/// </summary>
public class ProjectInformation(
	List<AssetTypeDefinition> assetTypeDefinitions,
	List<ComponentTypeDefinition> componentTypeDefinitions
)
{
	public List<AssetTypeDefinition> AssetTypeDefinitions { get; } = assetTypeDefinitions;
	public List<ComponentTypeDefinition> ComponentTypeDefinitions { get; } = componentTypeDefinitions;
}
