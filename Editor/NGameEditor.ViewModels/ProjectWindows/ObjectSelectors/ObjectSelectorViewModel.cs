using DynamicData.Binding;

namespace NGameEditor.ViewModels.ProjectWindows.ObjectSelectors;



public class ObjectSelectorViewModel(
	SelectedObjectViewModel selectedObject
) : ViewModelBase
{
	public string SearchFilter { get; set; } = "";
	public ObservableCollectionExtended<ObjectViewModel> FilteredObjects { get; } = [];
	public ObservableCollectionExtended<ObjectViewModel> SelectedObjects { get; } = [];
	public SelectedObjectViewModel SelectedObject { get; set; } = selectedObject;
}
