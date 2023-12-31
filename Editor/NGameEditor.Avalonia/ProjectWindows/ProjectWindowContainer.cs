using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using NGameEditor.Avalonia.Shared;
using NGameEditor.Functionality.Windows.ProjectWindow;

namespace NGameEditor.Avalonia.ProjectWindows;



public class ProjectWindowContainer(
	Func<ProjectWindow> factoryMethod,
	IFilePickerOpener filePickerOpener
) : IProjectWindow
{
	public event Action? Closing;


	public bool IsActive => ProjectWindow?.IsActive == true;

	private ProjectWindow? ProjectWindow { get; set; }
	private string ProjectName { get; set; } = "";
	private string SceneFileName { get; set; } = "";


	public void Open()
	{
		if (ProjectWindow != null) throw new InvalidOperationException();

		ProjectWindow = factoryMethod();
		ProjectWindow.Closing += (_, _) => Close();
		ProjectWindow.Show();
		UpdateTitle();
	}


	public void SetProjectName(string projectName)
	{
		ProjectName = projectName;
		UpdateTitle();
	}


	public void SetSceneName(string sceneFileName)
	{
		SceneFileName = sceneFileName;
		UpdateTitle();
	}


	public Task<IReadOnlyList<string>> AskUserToPickFile(OpenFileOptions openFileOptions)
	{
		var topLevel = TopLevel.GetTopLevel(ProjectWindow)!;
		var storageProvider = topLevel.StorageProvider;
		return filePickerOpener.OpenFilePicker(storageProvider, openFileOptions);
	}


	public Task<IReadOnlyList<string>> AskUserToPickFolder(OpenFolderOptions openFolderOptions)
	{
		var topLevel = TopLevel.GetTopLevel(ProjectWindow)!;
		var storageProvider = topLevel.StorageProvider;
		return filePickerOpener.OpenFolderPicker(storageProvider, openFolderOptions);
	}


	public void Close()
	{
		if (ProjectWindow == null) return;

		var projectWindow = ProjectWindow;
		ProjectWindow = null;

		Closing?.Invoke();
		projectWindow.Close();
	}


	private void UpdateTitle()
	{
		if (ProjectWindow == null) return;
		ProjectWindow.Title = $"{ProjectName} - {SceneFileName}";
	}
}
