namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;



public class CheckBoxEditorViewModel : EditorViewModel
{
	private bool? _value;

	public bool? Value
	{
		get => _value;
		set => this.RaiseAndSetIfChanged(ref _value, value);
	}
}
