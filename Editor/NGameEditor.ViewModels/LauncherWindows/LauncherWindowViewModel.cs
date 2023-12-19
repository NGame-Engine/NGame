using NGameEditor.ViewModels.LauncherWindows.HistoryViews;
using NGameEditor.ViewModels.LauncherWindows.Logs;

namespace NGameEditor.ViewModels.LauncherWindows;



public class LauncherWindowViewModel(
	ProjectOperationsViewModel projectOperationsViewModel,
	ProjectHistoryViewModel projectHistoryViewModel,
	LauncherLogViewModel launcherLogViewModel
)
	: ViewModelBase
{

	public ProjectOperationsViewModel ProjectOperationsViewModel { get; } = projectOperationsViewModel;
	public ProjectHistoryViewModel ProjectHistoryViewModel { get; } = projectHistoryViewModel;
	public LauncherLogViewModel LauncherLogViewModel { get; } = launcherLogViewModel;
}
