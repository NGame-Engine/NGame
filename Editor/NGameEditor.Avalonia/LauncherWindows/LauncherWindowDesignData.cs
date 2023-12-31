using NGameEditor.Avalonia.LauncherWindows.Logs;
using NGameEditor.Avalonia.LauncherWindows.ProjectHistoryViews;
using NGameEditor.Avalonia.LauncherWindows.ProjectOperationsViews;
using NGameEditor.ViewModels.LauncherWindows;

namespace NGameEditor.Avalonia.LauncherWindows;



public static class LauncherWindowDesignData
{
	public static LauncherWindowViewModel Example { get; } =
		new(
			ProjectOperationsDesignData.ProjectOperationsExample,
			ProjectHistoryDesignData.ProjectHistoryExample,
			LauncherLogDesignData.Example
		);
}
