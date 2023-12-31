using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Functionality.Projects;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IAddComponentMenuEntryFactory
{
	IEnumerable<MenuEntryViewModel> Create(EntityState entityState);
}



public class AddComponentMenuEntryFactory(
	IClientRunner<IBackendApi> clientRunner,
	ILogger<EntityStateMapper> logger,
	ProjectInformationState projectInformationState,
	IComponentStateMapper componentStateMapper
) : IAddComponentMenuEntryFactory
{
	public IEnumerable<MenuEntryViewModel> Create(EntityState entityState) =>
		projectInformationState
			.ProjectInformation
			.ComponentTypeDefinitions
			.Select(definition => CreateAddComponentMenuEntry(entityState, definition));


	private MenuEntryViewModel CreateAddComponentMenuEntry(EntityState entityState, ComponentTypeDefinition definition)
	{
		return new MenuEntryViewModel(
			definition.Name,
			ReactiveCommand.Create(() =>
				clientRunner
					.GetClientService()
					.Then(x => x.AddComponent(entityState.Id, definition))
					.Then(componentStateMapper.MapComponent)
					.Then(entityState.Components.Add)
					.IfError(logger.Log)
			)
		);
	}
}
