namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;



public class CheckBoxEditorViewModelOld : EditorViewModel
{
	private bool? _value;

	public bool? Value
	{
		get => _value;
		set => this.RaiseAndSetIfChanged(ref _value, value);
	}
}
