using System.Windows.Input;
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



public class ProjectOperationsViewModel : ViewModelBase, IActivatableViewModel
{
	public ViewModelActivator Activator { get; } = new();


	public ICommand? CreateNewProject { get; set; }

	public ICommand? OpenExistingProject { get; set; }
}
