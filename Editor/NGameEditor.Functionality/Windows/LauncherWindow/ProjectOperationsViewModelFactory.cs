using NGameEditor.Functionality.Projects;
using NGameEditor.ViewModels.LauncherWindows;
using ReactiveUI;

namespace NGameEditor.Functionality.Windows.LauncherWindow;



public interface IProjectOperationsViewModelFactory
{
	ProjectOperationsViewModel Create();
}



public class ProjectOperationsViewModelFactory(
	IExistingProjectOpener existingProjectOpener,
	IProjectCreator projectCreator
) : IProjectOperationsViewModelFactory
{
	public ProjectOperationsViewModel Create()
	{
		var projectOperationsViewModel = new ProjectOperationsViewModel();

		projectOperationsViewModel.CreateNewProject =
			ReactiveCommand.CreateFromTask<CreateProjectDialogArgs>(projectCreator.CreateProject);

		projectOperationsViewModel.OpenExistingProject =
			ReactiveCommand.CreateFromTask<OpenExistingProjectArgs>(existingProjectOpener.OnOpenExistingProject);

		return projectOperationsViewModel;
	}
}
