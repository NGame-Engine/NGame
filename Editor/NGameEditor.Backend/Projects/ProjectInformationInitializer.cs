using NGame.Assets;
using NGame.Ecs;
using NGameEditor.Bridge.Files;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Projects;
using NGameEditor.Bridge.Scenes;

namespace NGameEditor.Backend.Projects;



public class ProjectInformationInitializer(
	ProjectDefinition projectDefinition,
	IFrontendApi frontendApi
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
				.Select(x =>
					new AssetTypeDefinition(
						AssetAttribute.GetName(x),
						x.FullName!,
						x != typeof(Asset)
					)
				)
				.ToList();

		var projectInformation = new ProjectInformation(
			assetTypeDefinitions,
			componentTypeDefinitions
		);

		frontendApi.SetProjectInformation(projectInformation);
	}
}
