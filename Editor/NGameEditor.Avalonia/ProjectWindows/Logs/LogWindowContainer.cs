using System;
using NGameEditor.Functionality.Logging;
using NGameEditor.Functionality.Windows;

namespace NGameEditor.Avalonia.ProjectWindows.Logs;



public class LogWindowContainer(Func<LogWindow> factoryMethod, IProjectWindow projectWindow) : ILogWindow
{
	private LogWindow? LogWindow { get; set; }


	public void Open()
	{
		if (projectWindow.IsActive == false) return;


		if (LogWindow == null)
		{
			LogWindow = factoryMethod();
			LogWindow.Closed += (_, _) => Close();
			projectWindow.Closing += () => LogWindow?.Close();
		}

		LogWindow.Show();
	}


	private void Close()
	{
		if (LogWindow == null) return;

		LogWindow = null;
	}
}
