using System;
using NGameEditor.ViewModels.ProjectWindows.HierarchyViews;

namespace NGameEditor.Avalonia.ProjectWindows.HierarchyViews;



public static class HierarchyDesignData
{
	public static HierarchyViewModel Example { get; } =
		new()
		{
			SearchFilter = "Search phrase",
			SearchResults =
			{
				new EntityNodeViewModel(Guid.Empty, "First Entity", true),
				new EntityNodeViewModel(Guid.Empty, "Second Entity", false),
				new EntityNodeViewModel(Guid.Empty, "Paren Entity", true)
				{
					Children =
					{
						new EntityNodeViewModel(Guid.Empty, "First Child", true),
						new EntityNodeViewModel(Guid.Empty, "Second Child", true)
					}
				}
			}
		};
}
