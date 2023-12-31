using System.Collections.ObjectModel;
using System.Windows.Input;
using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.HierarchyViews;



public class HierarchyViewModel : ViewModelBase
{
	private string _searchFilter = "";


	public string SearchFilter
	{
		get => _searchFilter;
		set => this.RaiseAndSetIfChanged(ref _searchFilter, value);
	}

	public ObservableCollectionExtended<EntityNodeViewModel> SceneEntities { get; } = new();


	public ObservableAsPropertyHelper<IEnumerable<EntityNodeViewModel>> SearchResultsHelper { get; set; } = null!;
	public IEnumerable<EntityNodeViewModel> SearchResults => SearchResultsHelper.Value;

	public ObservableCollection<EntityNodeViewModel> SelectedEntities { get; } = new();


	public ICommand? AddEntity { get; set; }
}
