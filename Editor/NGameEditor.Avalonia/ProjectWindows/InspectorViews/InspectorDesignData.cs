using NGameEditor.Avalonia.Components.CustomEditors;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Avalonia.ProjectWindows.InspectorViews;



public static class InspectorDesignData
{
	public static InspectorViewModel Example { get; } =
		new()
		{
			Title = "Example Entity",
			//CanEditTitle = true,
			CustomEditors =
			{
				CustomEditorsDesignData.CheckBoxExample,
				CustomEditorsDesignData.TextEditorExample,
				CustomEditorsDesignData.UnrecognizedExample,
				CustomEditorsDesignData.StackPanelExample
			}
		};
}
