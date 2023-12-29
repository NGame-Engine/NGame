namespace NGameEditor.ViewModels.Components.CustomEditors;



public class CheckBoxEditorViewModel(bool isChecked) : ComponentEditorViewModel
{
	public bool IsChecked
	{
		get => isChecked;
		set => this.RaiseAndSetIfChanged(ref isChecked, value);
	}
}
