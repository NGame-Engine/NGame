using Microsoft.Extensions.Logging;
using NGameEditor.Functionality.Shared;
using NGameEditor.ViewModels.LauncherWindows.Logs;
using NGameEditor.ViewModels.ProjectWindows.Logs;

namespace NGameEditor.Functionality.Logging;



public class UiLogger(
	LogWindowModel logWindowModel,
	ILogWindow logWindow,
	LauncherLogViewModel launcherLogViewModel,
	IUiThreadDispatcher uiThreadDispatcher
) : ILogger
{
	public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;


	public bool IsEnabled(LogLevel logLevel) => true;


	public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, string> formatter
	)
	{
		uiThreadDispatcher.DoOnUiThread(
			() => Log(state, exception, formatter)
		);
	}


	private void Log<TState>(TState state, Exception? exception, Func<TState, Exception?, string> formatter)
	{
		var message = formatter(state, exception);
		var logEntryViewModel = new LogEntryViewModel(message, DateTime.Now);
		logWindowModel.Show(logEntryViewModel);
		logWindow.Open();

		var launcherLogEntryViewModel = new LauncherLogEntryViewModel(message, DateTime.Now);
		launcherLogViewModel.LogEntries.Add(launcherLogEntryViewModel);
	}
}
