using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public static class FileBrowserDesignData
{
	public static DirectoryViewModel DirectoryExample { get; } =
		new("Example Folder", new ContextMenuViewModel([]))
		{
			Directories =
			{
				new DirectoryViewModel("Fakefolder", new ContextMenuViewModel([])),
				new DirectoryViewModel("Fakefolder 2", new ContextMenuViewModel([])),
				new DirectoryViewModel("Fakefolder 3", new ContextMenuViewModel([]))
			},
			Files =
			{
				new FileViewModel("Example File 1.txt"),
				new FileViewModel("Example File 2.png")
			}
		};

	public static DirectoryOverviewViewModel DirectoryOverviewExample { get; } =
		new()
		{
			Directories =
			{
				DirectoryExample
			}
		};

	public static DirectoryContentViewModel DirectoryContentExample { get; } =
		new()
		{
			DirectoryName = "Selected Parent Folder",
			Directories =
			{
				DirectoryExample,
				DirectoryExample
			}
		};


	public static FileBrowserViewModel FileBrowserExample { get; } =
		new()
		{
			DirectoryOverviewViewModel = { Directories = { DirectoryExample } },
			DirectoryContentViewModel =
			{
				DirectoryName = "Selected Directory",
				Directories = { DirectoryExample }
			}
		};
}
