using NGameEditor.Functionality.Projects;
using NGameEditor.Functionality.Windows.LauncherWindow;
using NGameEditor.ViewModels.LauncherWindows.HistoryViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows;



public interface ILauncherWindowOpener
{
	void Open();
}



public class LauncherWindowOpener(
	ILauncherWindow launcherWindow,
	IProjectUsageRepository usageRepository,
	ProjectHistoryViewModel projectHistoryViewModel,
	IProjectOpener projectOpener
)
	: ILauncherWindowOpener
{
	public void Open()
	{
		var projectHistoryEntries =
			usageRepository
				.GetRecentlyOpenedProjects()
				.Select(x =>
					new HistoryEntryViewModel(
						x.ProjectId.SolutionFilePath.PathExport,
						x.LastUsed,
						ReactiveCommand.CreateFromTask(
							() => projectOpener.OpenProject(x.ProjectId)
						)
					)
				);

		projectHistoryViewModel.SetHistoryEntries(projectHistoryEntries);

		launcherWindow.Open();
	}
}
