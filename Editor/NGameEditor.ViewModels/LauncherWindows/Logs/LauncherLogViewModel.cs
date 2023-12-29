using System.Collections.ObjectModel;

namespace NGameEditor.ViewModels.LauncherWindows.Logs;



public class LauncherLogViewModel : ViewModelBase
{
	public ObservableCollection<LauncherLogEntryViewModel> LogEntries { get; } = new();
}
