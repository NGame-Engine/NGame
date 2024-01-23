using System.Windows.Input;

namespace NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;



public class ObjectViewModel(
	Guid id,
	string icon,
	string displayName,
	ICommand chooseObject
) : ViewModelBase
{
	public Guid Id { get; } = id;
	public string Icon { get; } = icon;
	public string DisplayName { get; } = displayName;

	public ICommand ChooseObject { get; } = chooseObject;
}
