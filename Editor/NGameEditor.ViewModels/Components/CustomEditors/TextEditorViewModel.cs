namespace NGameEditor.ViewModels.Components.CustomEditors;



public class TextEditorViewModel(string? text) : ComponentEditorViewModel
{
	public string? Text
	{
		get => text;
		set => this.RaiseAndSetIfChanged(ref text, value);
	}
}
