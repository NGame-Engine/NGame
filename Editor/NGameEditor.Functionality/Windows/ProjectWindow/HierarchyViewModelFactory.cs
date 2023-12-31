using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.Functionality.Scenes;
using NGameEditor.Functionality.Scenes.State;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.ProjectWindow;



public interface IHierarchyViewModelFactory
{
	HierarchyViewModel Create();
}



public class HierarchyViewModelFactory(
	IEntityCreator entityCreator,
	SceneState sceneState,
	SelectedEntitiesState selectedEntitiesState,
	IEntityNodeViewModelMapper entityNodeViewModelMapper
) : IHierarchyViewModelFactory
{
	public HierarchyViewModel Create()
	{
		var hierarchyViewModel = new HierarchyViewModel();


		var observablePredicate =
			hierarchyViewModel
				.WhenAnyValue(viewModel => viewModel.SearchFilter)
				.Select<string, Func<EntityState, bool>>(searchFilter =>
					entity => IsMatchingFilter(entity, searchFilter)
				);


		sceneState
			.SceneEntities
			.ToObservableChangeSet()
			.Filter(observablePredicate)
			.Transform(entityNodeViewModelMapper.Map)
			.Bind(hierarchyViewModel.SearchResults)
			.Subscribe();


		hierarchyViewModel
			.SelectedEntities
			.ToObservableChangeSet()
			.Transform(x =>
				sceneState
					.SceneEntities
					.First(entity => entity.Id == x.Id)
			)
			.Bind(selectedEntitiesState.SelectedEntities)
			.Subscribe();


		hierarchyViewModel.AddEntity = ReactiveCommand.Create(() => entityCreator.CreateEntity(null));


		return hierarchyViewModel;
	}


	private static bool IsMatchingFilter(EntityState entityState, string searchFilter) =>
		string.IsNullOrWhiteSpace(searchFilter) ||
		entityState.Name.Contains(searchFilter, StringComparison.OrdinalIgnoreCase);
}
