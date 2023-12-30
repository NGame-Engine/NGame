using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Projects;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Controllers;



public class EntityController(
	IComponentController componentController,
	IClientRunner<IBackendApi> clientRunner,
	ILogger<EntityStateMapper> logger,
	ProjectInformationState projectInformationState
) : IEntityController
{
	public IEnumerable<MenuEntryViewModel> GetAddComponentMenuEntries(EntityState entityState) =>
		projectInformationState
			.ProjectInformation
			.ComponentTypeDefinitions
			.Select(definition =>
				new MenuEntryViewModel(
					definition.Name,
					componentController.AddComponent(definition, entityState)
				)
			);


	public Result SetName(EntityState entity, string newName) =>
		clientRunner
			.GetClientService()
			.Then(x => x.SetEntityName(entity.Id, newName))
			.IfError(logger.Log);
}
