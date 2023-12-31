using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.Functionality.Scenes;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;
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
		var addEntityCommand = ReactiveCommand.Create(() => entityCreator.CreateEntity(null));
		var hierarchyViewModel = new HierarchyViewModel(addEntityCommand);

		hierarchyViewModel.AddEntity = addEntityCommand;

		sceneState
			.SceneEntities
			.ToObservableChangeSet()
			.Transform(entityNodeViewModelMapper.Map)
			.Filter(x => IsMatchingFilter(x, hierarchyViewModel.SearchFilter))
			.Bind(hierarchyViewModel.SceneEntities)
			.Subscribe();


		hierarchyViewModel
				.SearchResultsHelper =
			hierarchyViewModel
				.WhenAnyValue(x => x.SearchFilter).Select(_ => Unit.Default)
				.Merge(hierarchyViewModel.SceneEntities.ToObservableChangeSet().Select(_ => Unit.Default))
				.Throttle(TimeSpan.FromMilliseconds(100))
				.Select(_ =>
					hierarchyViewModel
						.SceneEntities
						.Where(entity => IsMatchingFilter(entity, hierarchyViewModel.SearchFilter))
				)
				.ObserveOn(RxApp.MainThreadScheduler)
				.ToProperty(hierarchyViewModel, x => x.SearchResults);


		hierarchyViewModel
			.SelectedEntities
			.ToObservableChangeSet()
			.Transform(x => x.EntityState)
			.Bind(selectedEntitiesState.SelectedEntities)
			.Subscribe();

		return hierarchyViewModel;
	}


	private static bool IsMatchingFilter(
		EntityNodeViewModel entityNodeViewModel,
		string searchFilter
	) =>
		string.IsNullOrWhiteSpace(searchFilter) ||
		entityNodeViewModel.Name.Contains(searchFilter, StringComparison.OrdinalIgnoreCase);
}
