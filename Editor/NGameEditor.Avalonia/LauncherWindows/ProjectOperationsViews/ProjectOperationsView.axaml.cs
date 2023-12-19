using Avalonia.Controls;
using NGameEditor.Avalonia.Shared;
using NGameEditor.ViewModels.LauncherWindows;

namespace NGameEditor.Avalonia.LauncherWindows.ProjectOperationsViews;



public partial class ProjectOperationsView : ReactiveUserControl<ProjectOperationsViewModel>
{
	public ProjectOperationsView()
	{
		this.WhenActivated(_ =>
		{
			var topLevel = TopLevel.GetTopLevel(this);
			var storageProvider = topLevel.StorageProvider;

			var filePicker = new FilePicker(storageProvider);
			var openExistingProjectArgs = new OpenExistingProjectArgs(filePicker);
			OpenProjectButton.CommandParameter = openExistingProjectArgs;


			var folderPicker = new FolderPicker(storageProvider);
			var createProjectDialogArgs = new CreateProjectDialogArgs(folderPicker);
			CreateNewProjectButton.CommandParameter = createProjectDialogArgs;
		});


		InitializeComponent();
	}
}
