using System.Collections.ObjectModel;
using DynamicData;

namespace NGameEditor.ViewModels.LauncherWindows.HistoryViews;



public class ProjectHistoryViewModel : ViewModelBase
{
	public ObservableCollection<HistoryEntryViewModel> ProjectUsages { get; } = [];


	private HistoryEntryViewModel? _selectedEntry;

	public HistoryEntryViewModel? SelectedEntry
	{
		get => _selectedEntry;
		set => this.RaiseAndSetIfChanged(ref _selectedEntry, value);
	}


	public void SetHistoryEntries(IEnumerable<HistoryEntryViewModel> historyEntryViewModels)
	{
		ProjectUsages.Clear();
		ProjectUsages.AddRange(historyEntryViewModels);
	}
}
