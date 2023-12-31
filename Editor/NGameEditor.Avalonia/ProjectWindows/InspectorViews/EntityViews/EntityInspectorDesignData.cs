using NGameEditor.Avalonia.Components.CustomEditors;
using NGameEditor.ViewModels.ProjectWindows.InspectorViews;

namespace NGameEditor.Avalonia.ProjectWindows.InspectorViews.EntityViews;



public static class EntityInspectorDesignData
{
	public static InspectorEntityViewModel Example { get; } =
		new()
		{
			EntityName = "Example Entity",
			CanEditName = true,
			ComponentEditors =
			{
				CustomEditorsDesignData.CheckBoxExample,
				CustomEditorsDesignData.TextEditorExample,
				CustomEditorsDesignData.UnrecognizedExample,
				CustomEditorsDesignData.StackPanelExample
			}
		};
}
