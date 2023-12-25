using NGameEditor.ViewModels.Components.Menus;
using NGameEditor.ViewModels.ProjectWindows.FileBrowsers;

namespace NGameEditor.Avalonia.ProjectWindows.FileBrowsers;




public static class FileBrowserDesignData
{

	public static FileBrowserViewModel FileBrowserExample { get; } =
		new()
		{
			Directories =
			{
				new DirectoryViewModel("Fakefolder", new ContextMenuViewModel([])),
				new DirectoryViewModel("Fakefolder 2", new ContextMenuViewModel([])),
				new DirectoryViewModel("Fakefolder 3", new ContextMenuViewModel([]))
			},
			Files =
			{
				new FileViewModel(),
				new FileViewModel()
			}
		};
}
