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

	public ObservableCollectionExtended<EntityNodeViewModel> SearchResults { get; } = [];
	public ObservableCollection<EntityNodeViewModel> SelectedEntities { get; } = [];


	public ICommand? AddEntity { get; set; }
}
