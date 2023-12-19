using System.Reactive;
using NGameEditor.ViewModels.Shared;

namespace NGameEditor.ViewModels.LauncherWindows;



public class CreateProjectDialogArgs(IFolderPicker folderPicker)
{
	public IFolderPicker FolderPicker { get; } = folderPicker;
}



public class OpenExistingProjectArgs(IFilePicker filePicker)
{
	public IFilePicker FilePicker { get; } = filePicker;
}



public class ProjectOperationsViewModel(
	IProjectController projectController
) : ViewModelBase, IActivatableViewModel
{
	public ViewModelActivator Activator { get; } = new();

	
	public ReactiveCommand<CreateProjectDialogArgs, Unit> CreateNewProject { get; set; } =
		projectController.CreateProject();

	public ReactiveCommand<OpenExistingProjectArgs, Unit> OpenExistingProject { get; set; } =
		projectController.OpenExistingProject();
}
