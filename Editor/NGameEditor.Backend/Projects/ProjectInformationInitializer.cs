using NGame.Ecs;
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

		var projectInformation = new ProjectInformation(componentTypeDefinitions);
		frontendApi.SetProjectInformation(projectInformation);
	}
}
