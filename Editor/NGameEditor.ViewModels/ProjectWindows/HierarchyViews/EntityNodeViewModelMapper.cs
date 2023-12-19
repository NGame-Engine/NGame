﻿using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public interface IEntityNodeViewModelMapper
{
	EntityNodeViewModel Map(EntityState entityState);
}



public class EntityNodeViewModelMapper(ISceneController sceneController, IEntityController entityController)
	: IEntityNodeViewModelMapper
{
	public EntityNodeViewModel Map(EntityState entityState) =>
		new(
			entityState,
			CreateContextMenu(entityState),
			Map
		);


	private ContextMenuViewModel CreateContextMenu(EntityState entityState) =>
		new(
			[
				new MenuEntryViewModel(
					"Add child",
					ReactiveCommand.Create(() =>
						sceneController.CreateEntity(entityState))
				),
				new MenuEntryViewModel(
					"Delete",
					ReactiveCommand.Create(() =>
						sceneController.Remove(entityState))
				),
				new MenuEntryViewModel(
					"Add component",
					entityController.GetAddComponentMenuEntries(entityState)
				)
			]
		);
}
