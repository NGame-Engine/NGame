namespace NGameEditor.ViewModels.Components.CustomEditors;



public class StackPanelEditorViewModel(
	IEnumerable<CustomEditorViewModel> items
) : CustomEditorViewModel
{
	public List<CustomEditorViewModel> Items { get; } = [..items];
}
