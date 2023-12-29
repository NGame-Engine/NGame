using DynamicData.Binding;

namespace NGameEditor.ViewModels.Components.Menus;



public class ContextMenuViewModel(IEnumerable<MenuEntryViewModel> children) : ViewModelBase
{
	public ObservableCollectionExtended<MenuEntryViewModel> Children { get; } = new(children);
}
