﻿using Microsoft.Extensions.Logging;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.Functionality.Controllers;



public class EntityController(
	IComponentController componentController,
	IBackendRunner backendRunner,
	ILogger<EntityStateMapper> logger
) : IEntityController
{
	public IEnumerable<MenuEntryViewModel> GetAddComponentMenuEntries(EntityState entityState)
	{
		var menuEntryResults = backendRunner
			.GetBackendService()
			.Then(x => x.GetComponentTypes())
			.Then(definitions => definitions
				.Select(definition =>
					new MenuEntryViewModel(
						definition.Name,
						componentController.AddComponent(definition, entityState)
					)
				)
				.ToList()
			);


		if (!menuEntryResults.HasError) return menuEntryResults.SuccessValue!;


		logger.Log(menuEntryResults.ErrorValue!);
		return new List<MenuEntryViewModel>();
	}


	public Result SetName(EntityState entity, string newName) =>
		backendRunner
			.GetBackendService()
			.Then(x => x.SetEntityName(entity.Id, newName))
			.IfError(logger.Log);
}