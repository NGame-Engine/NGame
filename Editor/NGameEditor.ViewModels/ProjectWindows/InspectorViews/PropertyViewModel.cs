namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class PropertyViewModel(
	string name,
	string value
) : ViewModelBase
{
	public string Name => $"{name}:";
	public string Value => value;
}
