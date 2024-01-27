using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.ObjectSelectors.State;



public class ObjectSelectionState : ViewModelBase
{
	public ObservableCollectionExtended<SelectableObjectState> AvailableObjects { get; } = [];
	public ObservableCollectionExtended<SelectableObjectState> SelectedObjects { get; } = [];
}
