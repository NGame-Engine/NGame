using NGame.Ecs;
using NGameEditor.Backend.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Projects;



public class ProjectInformationInitializer(
	ProjectDefinition projectDefinition,
	IFrontendApi frontendApi,
	IAssetTypeDefinitionMapper assetTypeDefinitionMapper
) : IBackendStartListener
{
	public int Priority => 100;


	public void OnBackendStarted()
	{
		var componentTypeDefinitions =
			projectDefinition
				.ComponentTypes
				.Select(x =>
					new ComponentTypeDefinition(
						ComponentAttribute.GetName(x),
						x.FullName!
					)
				)
				.ToList();

		var assetTypeDefinitions =
			projectDefinition
				.AssetTypes
				.Select(assetTypeDefinitionMapper.Map)
				.ToList();

		var projectInformation = new ProjectInformation(
			assetTypeDefinitions,
			componentTypeDefinitions
		);

		frontendApi.SetProjectInformation(projectInformation);
	}
}
