namespace NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;



public class ObjectViewModel(
	string icon,
	string displayName
) : ViewModelBase
{
	public string Icon { get; } = icon;
	public string DisplayName { get; } = displayName;
}
