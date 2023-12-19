using System.Reactive;

namespace NGameEditor.ViewModels.LauncherWindows;



public interface IProjectController
{
	ReactiveCommand<OpenExistingProjectArgs, Unit> OpenExistingProject();
	ReactiveCommand<CreateProjectDialogArgs, Unit> CreateProject();
}
