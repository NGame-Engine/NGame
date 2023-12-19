namespace NGameEditor.ViewModels.LauncherWindows.Logs;



public class LauncherLogEntryViewModel(
	string message,
	DateTime loggedAt
) : ViewModelBase
{
	public string Message { get; } = message;
	public DateTime LoggedAt { get; } = loggedAt;
	public string LoggedAtString => LoggedAt.ToString("u");
}
