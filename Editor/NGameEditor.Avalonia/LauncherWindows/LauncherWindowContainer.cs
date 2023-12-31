using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using NGameEditor.Avalonia.Shared;
using NGameEditor.Functionality.Windows.LauncherWindow;

namespace NGameEditor.Avalonia.LauncherWindows;



public class LauncherWindowContainer(
	Func<LauncherWindow> factoryMethod,
	IFilePickerOpener filePickerOpener
) : ILauncherWindow
{
	private LauncherWindow? LauncherWindow { get; set; }
	private IStorageProvider? StorageProvider { get; set; }


	public void Open()
	{
		if (LauncherWindow != null) throw new InvalidOperationException();

		LauncherWindow = factoryMethod();

		var topLevel = TopLevel.GetTopLevel(LauncherWindow) ?? throw new InvalidOperationException();
		StorageProvider = topLevel.StorageProvider ?? throw new InvalidOperationException();

		LauncherWindow.Show();
	}


	public Task<IReadOnlyList<string>> AskUserToPickFile(OpenFileOptions openFileOptions) =>
		filePickerOpener.OpenFilePicker(StorageProvider!, openFileOptions);


	public Task<IReadOnlyList<string>> AskUserToPickFolder(OpenFolderOptions openFolderOptions) =>
		filePickerOpener.OpenFolderPicker(StorageProvider!, openFolderOptions);


	public void Close()
	{
		if (LauncherWindow == null) throw new InvalidOperationException();

		LauncherWindow.Close();
		LauncherWindow = null;
	}
}
