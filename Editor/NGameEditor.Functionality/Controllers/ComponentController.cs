using System.Windows.Input;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Bridge.Scenes;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Results;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;
using ReactiveUI;

namespace NGameEditor.Functionality.Controllers;



public interface IComponentController
{
	ICommand AddComponent(
		ComponentTypeDefinition componentTypeDefinition,
		EntityState entityState
	);
}



public class ComponentController(
	IBackendRunner backendRunner,
	ILogger<ComponentController> logger,
	IComponentStateMapper componentStateMapper
) : IComponentController
{
	public ICommand AddComponent(
		ComponentTypeDefinition componentTypeDefinition,
		EntityState entityState
	) =>
		ReactiveCommand.Create(() =>
			backendRunner
				.GetBackendService()
				.Then(x => x.AddComponent(entityState.Id, componentTypeDefinition))
				.Then(componentStateMapper.MapComponent)
				.Then(entityState.Components.Add)
				.IfError(logger.Log)
		);
}
