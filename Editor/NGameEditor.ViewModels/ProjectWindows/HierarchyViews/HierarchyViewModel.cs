using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using NGameEditor.ViewModels.Controllers;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public class HierarchyViewModel : ViewModelBase
{
	private readonly ISceneController _sceneController;


	public HierarchyViewModel(
		ISceneController sceneController,
		SceneState sceneState,
		SelectedEntitiesState selectedEntitiesState,
		IEntityNodeViewModelMapper entityNodeViewModelMapper
	)
	{
		_sceneController = sceneController;

		sceneState
			.SceneEntities
			.ToObservableChangeSet()
			.Transform(entityNodeViewModelMapper.Map)
			.Bind(_sceneEntities)
			.Subscribe();


		_searchResults = this
			.WhenAnyValue(x => x.SearchFilter).Select(_ => Unit.Default)
			.Merge(_sceneEntities.ToObservableChangeSet().Select(_ => Unit.Default))
			.Throttle(TimeSpan.FromMilliseconds(100))
			.Select(_ =>
				_sceneEntities
					.Where(se =>
						string.IsNullOrWhiteSpace(SearchFilter) ||
						se.Name.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase)
					)
			)
			.ObserveOn(RxApp.MainThreadScheduler)
			.ToProperty(this, x => x.SearchResults);


		SelectedEntities
			.ToObservableChangeSet()
			.Transform(x => x.EntityState)
			.Bind(selectedEntitiesState.SelectedEntities)
			.Subscribe();
	}


	private readonly ObservableCollectionExtended<EntityNodeViewModel> _sceneEntities = new();


	private string _searchFilter = "";

	public string SearchFilter
	{
		get => _searchFilter;
		set => this.RaiseAndSetIfChanged(ref _searchFilter, value);
	}


	private readonly ObservableAsPropertyHelper<IEnumerable<EntityNodeViewModel>> _searchResults;
	public IEnumerable<EntityNodeViewModel> SearchResults => _searchResults.Value;

	public ObservableCollection<EntityNodeViewModel> SelectedEntities { get; set; } = new();


	public ICommand AddEntity => ReactiveCommand.Create(() =>
		_sceneController.CreateEntity(null));
}
