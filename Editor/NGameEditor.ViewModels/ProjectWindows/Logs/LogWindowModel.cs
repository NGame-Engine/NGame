using System.Collections.ObjectModel;

namespace NGameEditor.ViewModels.ProjectWindows.Logs;



public class LogWindowModel : ViewModelBase
{
	public ObservableCollection<LogEntryViewModel> LogEntries { get; } = [];


	public void Show(LogEntryViewModel logEntryViewModel)
	{
		LogEntries.Add(logEntryViewModel);
	}
}



public class LogEntryViewModel(string message, DateTime loggedAt) : ViewModelBase
{
	public string Message { get; } = message;
	public string LoggedAtString => loggedAt.ToString("u");
}
