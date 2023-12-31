using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using NGameEditor.Bridge;
using NGameEditor.Bridge.InterProcessCommunication;
using NGameEditor.Functionality.Scenes.State;
using NGameEditor.Functionality.Windows.ProjectWindow;
using NGameEditor.Results;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Scenes;



public interface IEntityNodeViewModelMapper
{
	EntityNodeViewModel Map(EntityState entityState);
}



public class EntityNodeViewModelMapper(
	IEntityCreator entityCreator,
	IAddComponentMenuEntryFactory addComponentMenuEntryFactory,
	IClientRunner<IBackendApi> clientRunner,
	SceneState sceneState,
	ILogger<EntityNodeViewModelMapper> logger
)
	: IEntityNodeViewModelMapper
{
	public EntityNodeViewModel Map(EntityState entityState)
	{
		var entityNodeViewModel =
			new EntityNodeViewModel(
				entityState.Id,
				entityState.Name,
				entityState.Components.All(x => x.IsRecognized)
			);

		entityNodeViewModel
			.ContextMenu
			.Children
			.AddRange(CreateContextMenuEntries(entityState));

		entityState
			.Children
			.ToObservableChangeSet()
			.Transform(Map)
			.Bind(entityNodeViewModel.Children)
			.Subscribe();

		return entityNodeViewModel;
	}


	private IEnumerable<MenuEntryViewModel> CreateContextMenuEntries(EntityState entityState)
	{
		return
		[
			new MenuEntryViewModel(
				"Add child",
				ReactiveCommand.Create(() =>
					entityCreator.CreateEntity(entityState))
			),
			new MenuEntryViewModel(
				"Delete",
				ReactiveCommand.Create(() =>
					clientRunner
						.GetClientService()
						.Then(x => x.RemoveEntity(entityState.Id))
						.Then(() =>
							sceneState
								.SceneEntities
								.FindCollectionWithEntity(entityState.Id)!
								.Remove(entityState)
						)
						.IfError(logger.Log)
				)
			),
			new MenuEntryViewModel(
				"Add component",
				addComponentMenuEntryFactory.Create(entityState)
			)
		];
	}
}
