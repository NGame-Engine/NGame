using System.Reactive.Linq;
using NGameEditor.ViewModels.LauncherWindows.HistoryViews;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.LauncherWindow;



public interface IProjectHistoryViewModelFactory
{
	ProjectHistoryViewModel Create();
}



public class ProjectHistoryViewModelFactory : IProjectHistoryViewModelFactory
{
	public ProjectHistoryViewModel Create()
	{
		var projectHistoryViewModel = new ProjectHistoryViewModel();

		projectHistoryViewModel
			.WhenAnyValue(x => x.SelectedEntry)
			.Where(x => x != null)
			.Do(x => x!.OpenProject())
			.Subscribe();

		return projectHistoryViewModel;
	}
}
