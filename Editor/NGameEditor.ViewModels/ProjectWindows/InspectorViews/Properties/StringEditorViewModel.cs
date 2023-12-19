namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews.Properties;



public class StringEditorViewModel : EditorViewModel
{
	private string? _value;

	public string? Value
	{
		get => _value;
		set => this.RaiseAndSetIfChanged(ref _value, value);
	}
}
