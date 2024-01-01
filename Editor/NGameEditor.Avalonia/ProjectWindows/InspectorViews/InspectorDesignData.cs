using NGameEditor.Avalonia.Components.CustomEditors;
using NGameEditor.Avalonia.ProjectWindows.InspectorViews.EntityViews;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Avalonia.ProjectWindows.InspectorViews;



public static class InspectorDesignData
{
	public static InspectorViewModel Example { get; } =
		new(EntityInspectorDesignData.Example)
		{
			Title = "Example Entity",
			CanEditTitle = true,
			CustomEditors =
			{
				CustomEditorsDesignData.CheckBoxExample,
				CustomEditorsDesignData.TextEditorExample,
				CustomEditorsDesignData.UnrecognizedExample,
				CustomEditorsDesignData.StackPanelExample
			}
		};
}
