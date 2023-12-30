using NGameEditor.Bridge.Files;
using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Functionality.Files;



public interface IFileBrowserUpdater
{
	void UpdateProjectFiles(DirectoryDescription rootDirectory);
}



public class FileBrowserUpdater(FileBrowserViewModel fileBrowserViewModel) : IFileBrowserUpdater
{
	public void UpdateProjectFiles(DirectoryDescription rootDirectory)
	{
		fileBrowserViewModel.DirectoryOverviewViewModel.Directories.Clear();
		fileBrowserViewModel
			.DirectoryOverviewViewModel
			.Directories
			.AddRange(
				rootDirectory
					.SubDirectories
					.Select(MapDirectoryDescriptionRecursively)
			);
	}


	private DirectoryViewModel MapDirectoryDescriptionRecursively(
		DirectoryDescription directoryDescription
	)
	{
		var directoryViewModel =
			new DirectoryViewModel(
				directoryDescription.Name,
				new ContextMenuViewModel([])
			);

		foreach (var subDirectory in directoryDescription.SubDirectories)
		{
			directoryViewModel
				.Directories
				.Add(MapDirectoryDescriptionRecursively(subDirectory));
		}

		foreach (var fileDescription in directoryDescription.Files)
		{
			directoryViewModel
				.Files
				.Add(new FileViewModel(fileDescription.Name));
		}

		return directoryViewModel;
	}
}
