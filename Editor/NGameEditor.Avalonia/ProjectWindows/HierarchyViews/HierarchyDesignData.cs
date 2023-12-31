using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;

namespace NGameEditor.Avalonia.ProjectWindows.HierarchyViews;



public static class HierarchyDesignData
{
	public static HierarchyViewModel Example { get; } =
		new()
		{
			SearchFilter = "Search phrase"
		};
}
