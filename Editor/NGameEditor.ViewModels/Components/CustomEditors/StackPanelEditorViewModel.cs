namespace NGameEditor.ViewModels.Components.CustomEditors;



public class StackPanelEditorViewModel(
	IEnumerable<ComponentEditorViewModel> items
) : ComponentEditorViewModel
{
	public List<ComponentEditorViewModel> Items { get; } = new(items);
}
