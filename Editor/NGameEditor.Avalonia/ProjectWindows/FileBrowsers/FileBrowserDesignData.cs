using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;



public static class FileBrowserDesignData
{
	public static DirectoryContentItemViewModel DirectoryContentItemExample { get; } =
		new();


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
				new FileViewModel("Example File 1.txt", null),
				new FileViewModel("Example File 2.png", null)
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
			Items =
			{
				DirectoryContentItemExample,
				DirectoryContentItemExample
			}
		};


	public static FileBrowserViewModel FileBrowserExample { get; } =
		new(
			DirectoryOverviewExample,
			DirectoryContentExample
		);
}
