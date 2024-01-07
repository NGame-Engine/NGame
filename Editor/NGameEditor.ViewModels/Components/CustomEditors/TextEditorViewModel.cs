namespace NGameEditor.ViewModels.Components.CustomEditors;



public class TextEditorViewModel(string? text) : CustomEditorViewModel
{
	public string? Text
	{
		get => text;
		set => this.RaiseAndSetIfChanged(ref text, value);
	}
}
