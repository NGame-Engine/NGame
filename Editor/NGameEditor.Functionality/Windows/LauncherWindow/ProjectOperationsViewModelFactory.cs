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
	public ProjectOperationsViewModel Create() =>
		new()
		{
			CreateNewProject = ReactiveCommand.CreateFromTask(projectCreator.CreateProject),
			OpenExistingProject = ReactiveCommand.CreateFromTask(existingProjectOpener.OnOpenExistingProject)
		};
}
