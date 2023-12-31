using System.Windows.Input;

namespace NGameEditor.ViewModels.LauncherWindows;



public class ProjectOperationsViewModel : ViewModelBase
{
	public ICommand? CreateNewProject { get; set; }

	public ICommand? OpenExistingProject { get; set; }
}
