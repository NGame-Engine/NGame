using System;
using NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;

namespace NGameEditor.Avalonia.ProjectWindows.ObjectSelectors;



public static class ObjectSelectorDesignData
{
	public static ObjectViewModel ObjectExample { get; } =
		new(
			Guid.NewGuid(),
			"❔",
			"Example Obj...",
			ReactiveCommand.Create(() => { })
		);

	public static SelectedObjectViewModel SelectedObjectExample { get; } =
		new()
		{
			FullName = "Full Name",
			KindName = "Example Asset",
			Path = "Examples/Assets/FullName.ngasset"
		};


	public static ObjectSelectorViewModel ObjectSelectorExample { get; } =
		new(SelectedObjectExample)
		{
			FilteredObjects =
			{
				ObjectExample,
				ObjectExample,
				ObjectExample,
				ObjectExample,
				ObjectExample
			}
		};
}
