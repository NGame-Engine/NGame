using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;

namespace NGameEditor.ViewModels.LauncherWindows.HistoryViews;



public class ProjectHistoryViewModel : ViewModelBase
{
	public ProjectHistoryViewModel()
	{
		this.WhenAnyValue(x => x.SelectedEntry)
			.Where(x => x != null)
			.Select(x => x!.OpenProject())
			.Subscribe();
	}


	public ObservableCollection<HistoryEntryViewModel> ProjectUsages { get; } = new();


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
