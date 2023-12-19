namespace NGameEditor.ViewModels.Components.Menus;



public class ContextMenuViewModel(IEnumerable<MenuEntryViewModel> children) : ViewModelBase
{
	public List<MenuEntryViewModel> Children { get; } = new(children);
}
