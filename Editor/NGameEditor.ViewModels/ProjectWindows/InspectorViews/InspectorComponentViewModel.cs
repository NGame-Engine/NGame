using System.Collections.ObjectModel;
using NGameEditor.ViewModels.ProjectWindows.SceneStates;

namespace NGameEditor.ViewModels.ProjectWindows.InspectorViews;



public class InspectorComponentViewModel(
	ComponentState componentState,
	IEnumerable<PropertyViewModel> properties
) : ViewModelBase
{
	public ComponentState ComponentState { get; } = componentState;
	public string Name { get; } = componentState.Name;
	public bool IsRecognized { get; } = componentState.IsRecognized;
	public ObservableCollection<PropertyViewModel> Properties { get; } = new(properties);
}
