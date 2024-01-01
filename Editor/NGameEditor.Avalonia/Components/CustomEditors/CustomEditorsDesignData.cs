using NGameEditor.ViewModels.Components.CustomEditors;

namespace NGameEditor.Avalonia.Components.CustomEditors;



public static class CustomEditorsDesignData
{
	public static CheckBoxEditorViewModel CheckBoxExample { get; } = new(true);


	public static TextEditorViewModel TextEditorExample { get; } = new("editable text");

	public static UnrecognizedCustomEditorViewModel UnrecognizedExample { get; } = new();

	public static StackPanelEditorViewModel StackPanelExample { get; } = new(
	[
		CheckBoxExample,
		TextEditorExample,
		UnrecognizedExample
	]);
}
